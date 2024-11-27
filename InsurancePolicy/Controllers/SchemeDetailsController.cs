using InsurancePolicy.Models;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchemeDetailsController : ControllerBase
    {
        private readonly ISchemeDetailsService _service;
        public SchemeDetailsController(ISchemeDetailsService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var schemeDetails = _service.GetAll();
            return Ok(schemeDetails);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var schemeDetails = _service.GetById(id);
            return Ok(schemeDetails);
        }

        [HttpPost]
        public IActionResult Add(SchemeDetails schemeDetails)
        {
            var newSchemeDetails = _service.Add(schemeDetails);
            return Ok(newSchemeDetails);
        }

        [HttpPut]
        public IActionResult Modify(SchemeDetails schemeDetails)
        {
            _service.Update(schemeDetails);
            return Ok(schemeDetails);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return Ok("Deleted Successfully!");
        }
    }
}
