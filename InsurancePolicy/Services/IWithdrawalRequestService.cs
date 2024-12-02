using InsurancePolicy.DTOs;

namespace InsurancePolicy.Services
{
    public interface IWithdrawalRequestService
    {
        Guid CreateRequest(WithdrawalRequestDto requestDto);
        void ApproveRequest(Guid requestId);
        void RejectRequest(Guid requestId);
        WithdrawalRequestResponseDto GetRequestById(Guid requestId);
        List<WithdrawalRequestResponseDto> GetAllRequests();
    }
}
