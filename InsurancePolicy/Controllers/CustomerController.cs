using InsurancePolicy.DTOs;
using InsurancePolicy.Services;
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
        public IActionResult Add(CustomerRequestDto customerRequestDto)
        {
            var newCustomerId = _service.Add(customerRequestDto);
            return Ok(new { CustomerId = newCustomerId, Message = "Customer added successfully." });
        }

        [HttpPut]
        public IActionResult Modify(CustomerRequestDto customerRequestDto)
        {
            _service.Update(customerRequestDto);
            return Ok(new { Message = "Customer updated successfully." });
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return Ok("Deleted Successfully!");
        }
    }
}
