using InsurancePolicy.Models;
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
        public IActionResult Add(Policy policy)
        {
            var newPolicy = _service.Add(policy);
            return Ok(newPolicy);
        }

        [HttpPut]
        public IActionResult Modify(Policy policy)
        {
            _service.Update(policy);
            return Ok(policy);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return Ok("Deleted Successfully!");
        }
    }
}
