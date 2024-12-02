using InsurancePolicy.DTOs;

namespace InsurancePolicy.Services
{
    public interface ICommissionService
    {
        Guid AddCommission(CommissionRequestDto commissionDto);
        List<CommissionResponseDto> GetCommissionsByAgent(Guid agentId);
        List<CommissionResponseDto> GetCommissionsByPolicy(Guid policyId);
        List<CommissionResponseDto> GetAllCommissions();
    }
}
