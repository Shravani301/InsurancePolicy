using InsurancePolicy.Models;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;
        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = _service.GetAll();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var customer = _service.GetById(id);
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            var newCustomer = _service.Add(customer);
            return Ok(newCustomer);
        }

        [HttpPut]
        public IActionResult Modify(Customer customer)
        {
            _service.Update(customer);
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return Ok("Deleted Successfully!");
        }
    }
}
