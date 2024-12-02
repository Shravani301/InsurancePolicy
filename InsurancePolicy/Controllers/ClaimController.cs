using InsurancePolicy.DTOs;
using InsurancePolicy.Services;
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

        [HttpPost]
        public IActionResult AddClaim(ClaimRequestDto requestDto)
        {
            var id = _service.AddClaim(requestDto);
            return Ok(new { ClaimId = id });
        }

        [HttpPut]
        public IActionResult UpdateClaim(ClaimRequestDto requestDto)
        {
            _service.UpdateClaim(requestDto);
            return Ok("Claim updated successfully.");
        }

        [HttpPut("{claimId}/approve")]
        public IActionResult ApproveClaim(Guid claimId)
        {
            _service.ApproveClaim(claimId);
            return Ok("Claim approved successfully.");
        }

        [HttpPut("{claimId}/reject")]
        public IActionResult RejectClaim(Guid claimId, [FromQuery] string rejectionReason)
        {
            _service.RejectClaim(claimId, rejectionReason);
            return Ok("Claim rejected successfully.");
        }

        [HttpGet("{claimId}")]
        public IActionResult GetClaimById(Guid claimId)
        {
            var claim = _service.GetClaimById(claimId);
            return Ok(claim);
        }

        [HttpGet]
        public IActionResult GetAllClaims()
        {
            var claims = _service.GetAllClaims();
            return Ok(claims);
        }
    }
}
