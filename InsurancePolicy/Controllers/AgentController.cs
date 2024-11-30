using InsurancePolicy.DTOs;
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

        [HttpGet]
        public IActionResult GetAll()
        {
            var agents = _service.GetAll();
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
            return Ok("Deleted Successfully!");
        }
    }
}
