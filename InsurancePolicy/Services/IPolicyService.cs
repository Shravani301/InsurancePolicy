using InsurancePolicy.DTOs;
using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface IPolicyService
    {
        public List<PolicyResponseDto> GetAll();
        public PolicyResponseDto GetById(Guid id);
        public Guid Add(PolicyRequestDto policy);
        public bool Update(PolicyRequestDto policy);
        public bool Delete(Guid id);
    }
}
