using InsurancePolicy.DTOs;
using InsurancePolicy.Helpers;
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
        public IActionResult GetAll([FromQuery] PageParameters pageParameters)
        {
            var customers = _service.GetAllPaginated(pageParameters);

            // Add pagination metadata to headers
            Response.Headers.Add("X-Total-Count", customers.TotalCount.ToString());
            Response.Headers.Add("X-Page-Size", customers.PageSize.ToString());
            Response.Headers.Add("X-Current-Page", customers.CurrentPage.ToString());
            Response.Headers.Add("X-Total-Pages", customers.TotalPages.ToString());
            Response.Headers.Add("X-Has-Next", customers.HasNext.ToString());
            Response.Headers.Add("X-Has-Previous", customers.HasPrevious.ToString());

            // Return the customers in the response body
            return Ok(customers);
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
        [HttpPut("activate")]
        public IActionResult Activate(Guid id)
        {
            _service.Activate(id);
            return Ok(id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return Ok("Deleted Successfully!");
        }
    }
}
