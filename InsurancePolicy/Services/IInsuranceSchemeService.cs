using InsurancePolicy.DTOs;
using InsurancePolicy.Helpers;
using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface IInsuranceSchemeService
    {
        PageList<InsuranceSchemeResponseDto> GetAllPaginated(PageParameters pageParameters);
        PageList<InsuranceSchemeResponseDto> GetAllByPlanIdPaginated(Guid planId, PageParameters pageParameters);
        InsuranceSchemeResponseDto GetById(Guid id);
        Guid Add(InsuranceSchemeRequestDto scheme);
        bool Update(InsuranceSchemeRequestDto scheme);
        bool Delete(Guid id);
    }
}
