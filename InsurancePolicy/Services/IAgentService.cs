using InsurancePolicy.DTOs;
using InsurancePolicy.Helpers;
using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface IAgentService
    {
        public List<AgentResponseDto> GetAll();
        public AgentResponseDto GetById(Guid id);
        public Guid Add(AgentRequestDto agent);
        public bool Update(AgentRequestDto agent);
        public bool Delete(Guid id);
        public void Activate(Guid agentId);
        List<AgentResponseDto> GetAgentsByCustomerId(Guid customerId); // New method
        PageList<AgentResponseDto> GetAllPaginated(PageParameters pageParameters);
        public PageList<AgentResponseDto> GetAgentsByCustomerId(Guid customerId, PageParameters pageParameters);

    }
}
