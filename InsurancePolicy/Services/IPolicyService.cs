using InsurancePolicy.DTOs;
using InsurancePolicy.Helpers;

namespace InsurancePolicy.Services
{
    public interface IPolicyService
    {
        public PageList<PolicyResponseDto> GetAll(PageParameters pageParameters);
        public PolicyResponseDto GetById(Guid id);
        public Guid Add(PolicyRequestDto policy);
        public bool Update(PolicyRequestDto policy);
        public bool Delete(Guid id);
        public PageList<PolicyResponseDto> GetPoliciesByAgentId(Guid agentId, PageParameters pageParameters);
        public PageList<PolicyResponseDto> GetPoliciesByCustomerId(Guid customerId, PageParameters pageParameters);
        public PageList<PolicyResponseDto> GetPoliciesBySchemeId(Guid schemeId, PageParameters pageParameters);
        public PageList<PolicyResponseDto> GetPoliciesByPlanId(Guid planId, PageParameters pageParameters);
    }
}
