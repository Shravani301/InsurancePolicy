using InsurancePolicy.DTOs;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WithdrawalRequestController : ControllerBase
    {
        private readonly IWithdrawalRequestService _service;

        public WithdrawalRequestController(IWithdrawalRequestService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CreateRequest(WithdrawalRequestDto requestDto)
        {
            var requestId = _service.CreateRequest(requestDto);
            return Ok(new { WithdrawalRequestId = requestId });
        }

        [HttpPut("{requestId}/approve")]
        public IActionResult ApproveRequest(Guid requestId)
        {
            _service.ApproveRequest(requestId);
            return Ok("Request approved successfully.");
        }

        [HttpPut("{requestId}/reject")]
        public IActionResult RejectRequest(Guid requestId)
        {
            _service.RejectRequest(requestId);
            return Ok("Request rejected successfully.");
        }

        [HttpGet("{requestId}")]
        public IActionResult GetRequestById(Guid requestId)
        {
            var request = _service.GetRequestById(requestId);
            return Ok(request);
        }
        [HttpGet("agent/{agentId}/total-commission")]
        public IActionResult GetTotalCommission(Guid agentId)
        {
            var totalCommission = _service.GetTotalCommission(agentId);
            return Ok(new { AgentId = agentId, TotalCommission = totalCommission });
        }


        [HttpGet]
        public IActionResult GetAllRequests()
        {
            var requests = _service.GetAllRequests();
            return Ok(requests);
        }
    }
}
