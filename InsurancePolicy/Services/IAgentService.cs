using InsurancePolicy.DTOs;
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
    }
}
