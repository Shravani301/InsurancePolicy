using InsurancePolicy.DTOs;
using InsurancePolicy.Helpers;
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

        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var customers = _service.GetAll();
        //    return Ok(customers);
        //}
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
