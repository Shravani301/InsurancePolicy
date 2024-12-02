using InsurancePolicy.DTOs;
using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface IInsurancePlanService
    {
        List<InsurancePlanResponseDto> GetAll();
        InsurancePlanResponseDto GetById(Guid id);
        Guid Add(InsurancePlanRequestDto plan);
        void Update(InsurancePlanRequestDto plan);
        void Delete(Guid id);
    }
}
