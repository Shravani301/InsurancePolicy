using InsurancePolicy.Models;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _service.GetAll();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var employee = _service.GetById(id);
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            var newEmployee = _service.Add(employee);
            return Ok(newEmployee);
        }

        [HttpPut]
        public IActionResult Modify(Employee employee)
        {
            _service.Update(employee);
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return Ok("Deleted Successfully!");
        }
    }
}
