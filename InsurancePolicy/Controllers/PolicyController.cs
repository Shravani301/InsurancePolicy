using InsurancePolicy.DTOs;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Http;
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
    }
}
