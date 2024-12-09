using InsurancePolicy.DTOs;
using InsurancePolicy.Helpers;
using InsurancePolicy.Models;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceSchemeController : ControllerBase
    {
        private readonly IInsuranceSchemeService _service;

        public InsuranceSchemeController(IInsuranceSchemeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] PageParameters pageParameters)
        {
            var schemes = _service.GetAllPaginated(pageParameters);
            Response.Headers.Add("X-Current-Page", schemes.CurrentPage.ToString());
            Response.Headers.Add("X-Total-Pages", schemes.TotalPages.ToString());
            Response.Headers.Add("X-Has-Next", schemes.HasNext.ToString());
            Response.Headers.Add("X-Has-Previous", schemes.HasPrevious.ToString());
            Response.Headers.Add("X-Total-Count", schemes.TotalCount.ToString());
            return Ok(schemes);
        }

        [HttpGet("Plan/{planId}")]
        public IActionResult GetAllByPlanId(Guid planId, [FromQuery] PageParameters pageParameters)
        {
            var schemes = _service.GetAllByPlanIdPaginated(planId, pageParameters);
            Response.Headers.Add("X-Current-Page", schemes.CurrentPage.ToString());
            Response.Headers.Add("X-Total-Pages", schemes.TotalPages.ToString());
            Response.Headers.Add("X-Has-Next", schemes.HasNext.ToString());
            Response.Headers.Add("X-Has-Previous", schemes.HasPrevious.ToString());
            Response.Headers.Add("X-Total-Count", schemes.TotalCount.ToString());
            return Ok(schemes);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var scheme = _service.GetById(id);
            return Ok(scheme);
        }

        [HttpPost]
        public IActionResult Add(InsuranceSchemeRequestDto scheme)
        {
            var newScheme = _service.Add(scheme);
            return Ok(newScheme);
        }

        [HttpPut]
        public IActionResult Modify(InsuranceSchemeRequestDto scheme)
        {
            _service.Update(scheme);
            return Ok(scheme);
        }

        [HttpGet("{schemeId}/customer/{customerId}/exists")]
        public IActionResult IsCustomerAssociatedWithScheme(Guid schemeId, Guid customerId)
        {
            var isAssociated = _service.IsCustomerAssociatedWithScheme(schemeId, customerId);
            return Ok(new { IsAssociated = isAssociated });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return Ok(new { Message = "Deleted Successfully!" });
        }
    }
}
