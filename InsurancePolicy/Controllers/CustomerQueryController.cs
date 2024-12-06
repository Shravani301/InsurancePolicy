using InsurancePolicy.DTOs;
using InsurancePolicy.Helpers;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerQueryController : ControllerBase
    {
        private readonly ICustomerQueryService _service;

        public CustomerQueryController(ICustomerQueryService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CreateQuery(CustomerQueryRequestDto queryDto)
        {
            var queryId = _service.AddQuery(queryDto);
            return Ok(new { QueryId = queryId });
        }

        [HttpPut]
        public IActionResult UpdateQuery(CustomerQueryRequestDto queryDto)
        {
            _service.UpdateQuery(queryDto);
            return Ok("Query updated successfully.");
        }

        [HttpPut("{queryId}/resolve")]
        public IActionResult ResolveQuery(Guid queryId, [FromQuery] string response, [FromQuery] Guid employeeId)
        {
            _service.ResolveQuery(queryId, response, employeeId);
            return Ok("Query resolved successfully.");
        }

        [HttpGet("{queryId}")]
        public IActionResult GetQueryById(Guid queryId)
        {
            var query = _service.GetQueryById(queryId);
            return Ok(query);
        }

        [HttpGet("customer/{customerId}")]
        public IActionResult GetAllQueriesByCustomer(Guid customerId)
        {
            var queries = _service.GetAllQueriesByCustomer(customerId);
            return Ok(queries);
        }

        [HttpGet]
        public IActionResult GetAllPaginated([FromQuery] PageParameters pageParameters)
        {
            var queries = _service.GetPaginatedQueries(pageParameters);

            // Add pagination details in headers
            Response.Headers.Add("X-Total-Count", queries.TotalCount.ToString());
            Response.Headers.Add("X-Total-Pages", queries.TotalPages.ToString());
            Response.Headers.Add("X-Current-Page", queries.CurrentPage.ToString());
            Response.Headers.Add("X-Has-Next", queries.HasNext.ToString());
            Response.Headers.Add("X-Has-Previous", queries.HasPrevious.ToString());

            return Ok(queries);
        }
    }
}
