using InsurancePolicy.DTOs;
using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface IInsuranceSchemeService
    {
        public List<InsuranceSchemeResponseDto> GetAll();
        public InsuranceSchemeResponseDto GetById(Guid id);
        public Guid Add(InsuranceSchemeRequestDto scheme);
        public bool Update(InsuranceSchemeRequestDto scheme);
        public bool Delete(Guid id);
    }
}
