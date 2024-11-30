using InsurancePolicy.Data;
using InsurancePolicy.DTOs;
using InsurancePolicy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            var existingUser = _context.Users.Include(user => user.Role).FirstOrDefault(a => a.UserName == loginDto.UserName);
            if (existingUser != null)
            {
                //if (BCrypt.Net.BCrypt.Verify(loginDto.Password, existingUser.PasswordHash))
                //{

                //    var token = CreateToken(existingUser);
                //    Response.Headers.Add("Jwt", token);
                    return Ok(new { rollName = existingUser.Role.Name });
                //}

            }
            return BadRequest("Invalid username/password");
        }
        //private string CreateToken(User user)
        //{
        //    var roleName = user.Role.Name;
        //    List<Claim> claims = new List<Claim>()
        //    {
        //        new Claim(ClaimTypes.Name,user.UserName),
        //        new Claim(ClaimTypes.Role,roleName),
        //    };
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Key").Value));
        //    var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
        //    //token construction
        //    var token = new JwtSecurityToken(
        //        claims: claims,
        //        expires: DateTime.Now.AddDays(1),
        //        signingCredentials: cred
        //        );
        //    //generate the token
        //    var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        //    return jwt;
        //}
    }
}
