using InsurancePolicy.DTOs;

namespace InsurancePolicy.Services
{
    public interface IInstallmentService
    {
        Guid AddInstallment(InstallmentRequestDto installmentDto);
        bool UpdateInstallment(InstallmentRequestDto installmentDto);
        InstallmentResponseDto GetInstallmentById(Guid installmentId);
        List<InstallmentResponseDto> GetAllInstallmentsForPolicy(Guid policyId);
        bool DeleteInstallment(Guid installmentId);
    }
}
