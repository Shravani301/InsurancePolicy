using InsurancePolicy.Models;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _service;
        public ClaimController(IClaimService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var claims = _service.GetAll();
            return Ok(claims);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var claim = _service.GetById(id);
            return Ok(claim);
        }

        [HttpPost]
        public IActionResult Add(Claim claim)
        {
            var newClaim = _service.Add(claim);
            return Ok(newClaim);
        }

        [HttpPut]
        public IActionResult Modify(Claim claim)
        {
            _service.Update(claim);
            return Ok(claim);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return Ok("Deleted Successfully!");
        }
    }
}
