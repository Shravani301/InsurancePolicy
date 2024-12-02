using InsurancePolicy.DTOs;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InsurancePolicy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommissionController : ControllerBase
    {
        private readonly ICommissionService _commissionService;

        public CommissionController(ICommissionService commissionService)
        {
            _commissionService = commissionService;
        }

        [HttpPost]
        public IActionResult AddCommission(CommissionRequestDto commissionDto)
        {
            var commissionId = _commissionService.AddCommission(commissionDto);
            return Ok(new { CommissionId = commissionId, Message = "Commission added successfully." });
        }

        [HttpGet("agent/{agentId}")]
        public IActionResult GetCommissionsByAgent(Guid agentId)
        {
            var commissions = _commissionService.GetCommissionsByAgent(agentId);
            return Ok(commissions);
        }

        [HttpGet("policy/{policyId}")]
        public IActionResult GetCommissionsByPolicy(Guid policyId)
        {
            var commissions = _commissionService.GetCommissionsByPolicy(policyId);
            return Ok(commissions);
        }

        [HttpGet]
        public IActionResult GetAllCommissions()
        {
            var commissions = _commissionService.GetAllCommissions();
            return Ok(commissions);
        }
    }
}
