using InsurancePolicy.DTOs;
using InsurancePolicy.Models;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsurancePlanController : ControllerBase
    {
        private readonly IInsurancePlanService _service;
        public InsurancePlanController(IInsurancePlanService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var plans = _service.GetAll();
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var plan = _service.GetById(id);
            return Ok(plan);
        }

        [HttpPost]
        public IActionResult Add(InsurancePlanRequestDto plan)
        {
            var newPlan = _service.Add(plan);
            return Ok(newPlan);
        }

        [HttpPut]
        public IActionResult Modify(InsurancePlanRequestDto plan)
        {
            _service.Update(plan);
            return Ok(plan);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return Ok("Deleted Successfully!");
        }
    }
}
