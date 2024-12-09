using InsurancePolicy.DTOs;
using InsurancePolicy.Helpers;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IAgentService _service;

        public AgentController(IAgentService service)
        {
            _service = service;
        }
        [HttpPut("activate")]
        public IActionResult Activate(Guid id)
        {
            _service.Activate(id);
            return Ok(id);
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] PageParameters pageParameters)
        {
            var agents = _service.GetAllPaginated(pageParameters);

            // Add pagination metadata to headers
            Response.Headers.Add("X-Total-Count", agents.TotalCount.ToString());
            Response.Headers.Add("X-Page-Size", agents.PageSize.ToString());
            Response.Headers.Add("X-Current-Page", agents.CurrentPage.ToString());
            Response.Headers.Add("X-Total-Pages", agents.TotalPages.ToString());
            Response.Headers.Add("X-Has-Next", agents.HasNext.ToString());
            Response.Headers.Add("X-Has-Previous", agents.HasPrevious.ToString());

            // Return the customers in the response body
            return Ok(agents);
        }
        [HttpGet("customer/{customerId}")]
        public IActionResult GetAgentsByCustomerId(Guid customerId, [FromQuery] PageParameters pageParameters)
        {
            var agents = _service.GetAgentsByCustomerId(customerId, pageParameters);

            // Add pagination metadata to headers
            Response.Headers.Add("X-Total-Count", agents.TotalCount.ToString());
            Response.Headers.Add("X-Page-Size", agents.PageSize.ToString());
            Response.Headers.Add("X-Current-Page", agents.CurrentPage.ToString());
            Response.Headers.Add("X-Total-Pages", agents.TotalPages.ToString());
            Response.Headers.Add("X-Has-Next", agents.HasNext.ToString());
            Response.Headers.Add("X-Has-Previous", agents.HasPrevious.ToString());

            // Return paginated agents
            return Ok(agents);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var agent = _service.GetById(id);
            return Ok(agent);
        }

        [HttpPost]
        public IActionResult Add(AgentRequestDto agentRequestDto)
        {
            var newAgentId = _service.Add(agentRequestDto);
            return Ok(new { AgentId = newAgentId, Message = "Agent added successfully" });
        }

        [HttpPut]
        public IActionResult Modify(AgentRequestDto agentRequestDto)
        {
            _service.Update(agentRequestDto);
            return Ok(new { Message = "Agent updated successfully" });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return Ok(new { Message="Deleted Successfully!" });
        }
    }
}
