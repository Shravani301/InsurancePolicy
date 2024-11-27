using InsurancePolicy.Models;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;
        public AdminController(IAdminService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var admins = _service.GetAll();
            return Ok(admins);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var admin = _service.GetById(id);
            return Ok(admin);
        }

        [HttpPost]
        public IActionResult Add(Admin admin)
        {
            var newAdmin = _service.Add(admin);
            return Ok(newAdmin);
        }

        [HttpPut]
        public IActionResult Modify(Admin admin)
        {
            _service.Update(admin);
            return Ok(admin);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return Ok("Deleted Successfully!");
        }
    }
}
