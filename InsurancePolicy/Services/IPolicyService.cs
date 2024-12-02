using InsurancePolicy.DTOs;

namespace InsurancePolicy.Services
{
    public interface IPolicyService
    {
        public List<PolicyResponseDto> GetAll();
        public PolicyResponseDto GetById(Guid id);
        public Guid Add(PolicyRequestDto policy);
        public bool Update(PolicyRequestDto policy);
        public bool Delete(Guid id);
        public List<PolicyResponseDto> GetPoliciesByAgentId(Guid agentId);
        public List<PolicyResponseDto> GetPoliciesByCustomerId(Guid customerId);
        public List<PolicyResponseDto> GetPoliciesBySchemeId(Guid schemeId);
        public List<PolicyResponseDto> GetPoliciesByPlanId(Guid planId);
    }
}
