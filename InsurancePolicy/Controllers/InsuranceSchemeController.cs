using InsurancePolicy.Models;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Http;
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
        public IActionResult GetAll()
        {
            var schemes = _service.GetAll();
            return Ok(schemes);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var scheme = _service.GetById(id);
            return Ok(scheme);
        }

        [HttpPost]
        public IActionResult Add(InsuranceScheme scheme)
        {
            var newScheme = _service.Add(scheme);
            return Ok(newScheme);
        }

        [HttpPut]
        public IActionResult Modify(InsuranceScheme scheme)
        {
            _service.Update(scheme);
            return Ok(scheme);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return Ok("Deleted Successfully!");
        }
    }
}
