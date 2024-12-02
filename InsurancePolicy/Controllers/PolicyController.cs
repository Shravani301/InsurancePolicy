using InsurancePolicy.DTOs;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _service;

        public PolicyController(IPolicyService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var policies = _service.GetAll();
            return Ok(policies);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var policy = _service.GetById(id);
            return Ok(policy);
        }

        [HttpPost]
        public IActionResult Add(PolicyRequestDto policy)
        {
            var newPolicyId = _service.Add(policy);
            return Ok(newPolicyId);
        }

        [HttpPut]
        public IActionResult Update(PolicyRequestDto policy)
        {
            _service.Update(policy);
            return Ok("Policy updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return Ok("Policy deleted successfully.");
        }

        [HttpGet("agent/{agentId}")]
        public IActionResult GetPoliciesByAgentId(Guid agentId)
        {
            var policies = _service.GetPoliciesByAgentId(agentId);
            return Ok(policies);
        }

        [HttpGet("customer/{customerId}")]
        public IActionResult GetPoliciesByCustomerId(Guid customerId)
        {
            var policies = _service.GetPoliciesByCustomerId(customerId);
            return Ok(policies);
        }

        [HttpGet("scheme/{schemeId}")]
        public IActionResult GetPoliciesBySchemeId(Guid schemeId)
        {
            var policies = _service.GetPoliciesBySchemeId(schemeId);
            return Ok(policies);
        }

        [HttpGet("plan/{planId}")]
        public IActionResult GetPoliciesByPlanId(Guid planId)
        {
            var policies = _service.GetPoliciesByPlanId(planId);
            return Ok(policies);
        }
    }
}
