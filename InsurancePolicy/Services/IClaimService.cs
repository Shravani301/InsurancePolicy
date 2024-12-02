using InsurancePolicy.DTOs;

namespace InsurancePolicy.Services
{
    public interface IClaimService
    {
        Guid AddClaim(ClaimRequestDto requestDto);
        void UpdateClaim(ClaimRequestDto requestDto);
        void ApproveClaim(Guid claimId);
        void RejectClaim(Guid claimId, string rejectionReason);
        ClaimResponseDto GetClaimById(Guid claimId);
        List<ClaimResponseDto> GetAllClaims();
    }
}
