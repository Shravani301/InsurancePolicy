using InsurancePolicy.DTOs;
using InsurancePolicy.Helpers;
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
        public IActionResult GetAll([FromQuery] PageParameters pageParameters)
        {
            var claims = _service.GetAllPaginated(pageParameters);

            // Add pagination metadata to headers
            Response.Headers.Add("X-Total-Count", claims.TotalCount.ToString());
            Response.Headers.Add("X-Page-Size", claims.PageSize.ToString());
            Response.Headers.Add("X-Current-Page", claims.CurrentPage.ToString());
            Response.Headers.Add("X-Total-Pages", claims.TotalPages.ToString());
            Response.Headers.Add("X-Has-Next", claims.HasNext.ToString());
            Response.Headers.Add("X-Has-Previous", claims.HasPrevious.ToString());

            // Return the customers in the response body
            return Ok(claims);
        }
        [HttpGet("customer/{customerId}")]
        public IActionResult GetClaimsByCustomer(Guid customerId, [FromQuery] PageParameters pageParameters)
        {
            var claims = _service.GetClaimsByCustomerId(customerId, pageParameters);

            // Add pagination metadata to headers
            Response.Headers.Add("X-Total-Count", claims.TotalCount.ToString());
            Response.Headers.Add("X-Page-Size", claims.PageSize.ToString());
            Response.Headers.Add("X-Current-Page", claims.CurrentPage.ToString());
            Response.Headers.Add("X-Total-Pages", claims.TotalPages.ToString());
            Response.Headers.Add("X-Has-Next", claims.HasNext.ToString());
            Response.Headers.Add("X-Has-Previous", claims.HasPrevious.ToString());

            return Ok(claims);
        }
    }
}
