using AutoMapper;
using InsurancePolicy.DTOs;
using InsurancePolicy.enums;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class WithdrawalRequestService : IWithdrawalRequestService
    {
        private readonly IRepository<WithdrawalRequest> _withdrawalRequestRepository;
        private readonly IRepository<Commission> _commissionRepository;
        private readonly IMapper _mapper;

        public WithdrawalRequestService(
            IRepository<WithdrawalRequest> withdrawalRequestRepository,
            IRepository<Commission> commissionRepository,
            IMapper mapper)
        {
            _withdrawalRequestRepository = withdrawalRequestRepository;
            _commissionRepository = commissionRepository;
            _mapper = mapper;
        }

        public Guid CreateRequest(WithdrawalRequestDto requestDto)
        {
            var withdrawalRequest = _mapper.Map<WithdrawalRequest>(requestDto);

            // Calculate total commission for the agent
            var totalCommission = _commissionRepository.GetAll()
                .Where(c => c.AgentId == requestDto.AgentId)
                .Sum(c => c.Amount);

            withdrawalRequest.TotalCommission = totalCommission;
            withdrawalRequest.Status = WithdrawalRequestStatus.PENDING;

            _withdrawalRequestRepository.Add(withdrawalRequest);
            return withdrawalRequest.WithdrawalRequestId;
        }

        public void ApproveRequest(Guid requestId)
        {
            var request = _withdrawalRequestRepository.GetById(requestId);
            if (request == null)
                throw new KeyNotFoundException("Withdrawal request not found.");

            if (request.Status != WithdrawalRequestStatus.PENDING)
                throw new InvalidOperationException("Only pending requests can be approved.");

            // Deduct the amount from the agent's total commission
            var totalCommission = _commissionRepository.GetAll()
                .Where(c => c.AgentId == request.AgentId)
                .Sum(c => c.Amount);

            if (request.Amount > totalCommission)
                throw new InvalidOperationException("Insufficient commission balance.");

            // Update the status and deduct commission
            request.Status = WithdrawalRequestStatus.APPROVED;
            request.ApprovedAt = DateTime.Now;

            var commissions = _commissionRepository.GetAll()
                .Where(c => c.AgentId == request.AgentId)
                .ToList();

            double remainingAmount = request.Amount;
            foreach (var commission in commissions)
            {
                if (remainingAmount <= 0) break;

                if (commission.Amount >= remainingAmount)
                {
                    commission.Amount -= remainingAmount;
                    _commissionRepository.Update(commission);
                    break;
                }
                else
                {
                    remainingAmount -= commission.Amount;
                    commission.Amount = 0;
                    _commissionRepository.Update(commission);
                }
            }

            _withdrawalRequestRepository.Update(request);
        }

        public void RejectRequest(Guid requestId)
        {
            var request = _withdrawalRequestRepository.GetById(requestId);
            if (request == null)
                throw new KeyNotFoundException("Withdrawal request not found.");

            if (request.Status != WithdrawalRequestStatus.PENDING)
                throw new InvalidOperationException("Only pending requests can be rejected.");

            request.Status = WithdrawalRequestStatus.REJECTED;
            _withdrawalRequestRepository.Update(request);
        }

        public WithdrawalRequestResponseDto GetRequestById(Guid requestId)
        {
            var request = _withdrawalRequestRepository.GetAll()
                .Include(w => w.Agent)
                .Include(w => w.Customer)
                .FirstOrDefault(w => w.WithdrawalRequestId == requestId);

            if (request == null)
                throw new KeyNotFoundException("Withdrawal request not found.");

            return _mapper.Map<WithdrawalRequestResponseDto>(request);
        }

        public List<WithdrawalRequestResponseDto> GetAllRequests()
        {
            var requests = _withdrawalRequestRepository.GetAll()
                .Include(w => w.Agent)
                .Include(w => w.Customer)
                .ToList();

            return _mapper.Map<List<WithdrawalRequestResponseDto>>(requests);
        }
    }
}
