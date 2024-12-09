using InsurancePolicy.Data;
using InsurancePolicy.DTOs;
using InsurancePolicy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Claim = System.Security.Claims.Claim;
using System.Text;
using System.Security.Claims;

namespace InsurancePolicy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public LoginController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult Login(LoginDto loginDto)
        {
            var existingUser = _context.Users.Include(user => user.Role).Include(c=>c.Customer).FirstOrDefault(a => a.UserName == loginDto.UserName);
            if (existingUser != null)
            {
                if (BCrypt.Net.BCrypt.Verify(loginDto.Password, existingUser.Password))
                {

                    var token = CreateToken(existingUser);
                    Response.Headers.Add("Jwt", token);
                    if(existingUser.Role.Name=="Customer")
                            return Ok(new { roleName = existingUser.Role.Name, customerId=existingUser.Customer.CustomerId });
                    return Ok(new { roleName = existingUser.Role.Name });
                }

            }
            return BadRequest("Invalid username/password");
        }
        private string CreateToken(User user)
        {
            var roleName = user.Role.Name;
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Role,roleName),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Key").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            //token construction
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            //generate the token
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword([FromBody] PasswordChangeDto changePasswordDto)
        {
            // Find the user by their username
            var existingUser = _context.Users.FirstOrDefault(u => u.UserName == changePasswordDto.UserName);
            if (existingUser == null)
            {
                return BadRequest("User not found.");
            }

            // Verify the old password
            if (!BCrypt.Net.BCrypt.Verify(changePasswordDto.OldPassword, existingUser.Password))
            {
                return BadRequest("Old password is incorrect.");
            }

            // Hash the new password
            var newHashedPassword = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);

            // Overwrite the user's password with the new hash
            existingUser.Password = newHashedPassword;

            // Save changes to the database
            _context.Users.Update(existingUser);
            _context.SaveChanges();

            return Ok(new { Message = "Password changed successfully." });
        }
    }
}
