using InsurancePolicy.Models;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Http;
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
        public IActionResult Add(Agent agent)
        {
            var newAgent = _service.Add(agent);
            return Ok(newAgent);
        }

        [HttpPut]
        public IActionResult Modify(Agent agent)
        {
            _service.Update(agent);
            return Ok(agent);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _service.Delete(id);
            return Ok("Deleted Successfully!");
        }
    }
}
