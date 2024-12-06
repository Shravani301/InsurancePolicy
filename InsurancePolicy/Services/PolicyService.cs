using AutoMapper;
using InsurancePolicy.Data;
using InsurancePolicy.DTOs;
using InsurancePolicy.enums;
using InsurancePolicy.Exceptions.PolicyExceptions;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly IRepository<Policy> _policyRepository;
        private readonly IRepository<Installment> _installmentRepository;
        private readonly IRepository<Commission> _commissionRepository;
        private readonly IRepository<Nominee> _nomineeRepository;
        private readonly IRepository<InsuranceScheme> _insuranceSchemeRepository;
        private readonly IRepository<TaxSettings> _taxSettingsRepository;
        private readonly IRepository<InsuranceSettings> _insuranceSettingsRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public PolicyService(
            IRepository<Policy> policyRepository,
            IRepository<Installment> installmentRepository,
            IRepository<Commission> commissionRepository,
            IRepository<Nominee> nomineeRepository,
            IRepository<InsuranceScheme> insuranceSchemeRepository,
            IRepository<TaxSettings> taxSettingsRepository,
            IRepository<InsuranceSettings> insuranceSettingsRepository,
            IMapper mapper, AppDbContext context)
        {
            _policyRepository = policyRepository;
            _installmentRepository = installmentRepository;
            _commissionRepository = commissionRepository;
            _nomineeRepository = nomineeRepository;
            _insuranceSchemeRepository = insuranceSchemeRepository;
            _taxSettingsRepository = taxSettingsRepository;
            _insuranceSettingsRepository = insuranceSettingsRepository;
            _mapper = mapper;
            _context = context;
        }

        public List<PolicyResponseDto> GetAll()
        {
            var policies = _policyRepository.GetAll()
                .Include(p => p.InsuranceScheme).ThenInclude(p=>p.InsurancePlan)
                .Include(p => p.Nominees)
                .Include(p => p.Installments)
                .Include(p => p.Payments)
                .Include(p => p.Claim)
                .ToList();

            return _mapper.Map<List<PolicyResponseDto>>(policies);
        }

        public PolicyResponseDto GetById(Guid id)
        {
            var policy = _policyRepository.GetAll()
                .Include(p => p.InsuranceScheme)
                .Include(p => p.Nominees)
                .Include(p => p.Installments)
                .Include(p => p.Payments)
                .Include(p => p.Claim)
                .FirstOrDefault(p => p.PolicyId == id);

            if (policy == null)
                throw new PolicyNotFoundException("Policy not found.");

            return _mapper.Map<PolicyResponseDto>(policy);
        }

        public Guid Add(PolicyRequestDto requestDto)
        {
            var policy = _mapper.Map<Policy>(requestDto);

            // Set default values and validate relationships
            policy.Status = PolicyStatus.ACTIVE;
            policy.IssueDate = DateTime.Now;

            // Validate and set Insurance Scheme
            var insuranceScheme = _insuranceSchemeRepository.GetById(requestDto.InsuranceSchemeId);
            if (insuranceScheme == null)
                throw new InvalidOperationException("Invalid Insurance Scheme ID.");
            policy.InsuranceScheme = insuranceScheme;

            // Validate and set Tax Settings
            if (requestDto.TaxId.HasValue)
            {
                var taxSettings = _taxSettingsRepository.GetById(requestDto.TaxId.Value);
                if (taxSettings == null)
                    throw new InvalidOperationException("Invalid TaxSettings ID.");
                policy.TaxSettings = taxSettings;
            }

            // Validate and set Insurance Settings
            if (requestDto.InsuranceSettingId.HasValue)
            {
                var insuranceSettings = _insuranceSettingsRepository.GetById(requestDto.InsuranceSettingId.Value);
                if (insuranceSettings == null)
                    throw new InvalidOperationException("Invalid InsuranceSettings ID.");
                policy.InsuranceSettings = insuranceSettings;
            }

            // Add Nominees
            if (requestDto.Nominees != null)
            {
                policy.Nominees = requestDto.Nominees.Select(n => _mapper.Map<Nominee>(n)).ToList();
            }

            // Generate Installments
            policy.Installments = GenerateInstallments(policy);

            // Add Policy to the repository
            _policyRepository.Add(policy);

            // Save changes through repository
            _context.SaveChanges();

            // Update Commissions if Agent exists
            if (policy.AgentId.HasValue)
            {
                AddCommissionForAgent(policy);
            }

            return policy.PolicyId;
        }

        public bool Update(PolicyRequestDto requestDto)
        {
            var policy = _policyRepository.GetById(requestDto.PolicyId.Value);
            if (policy == null)
                throw new PolicyNotFoundException("Policy not found.");

            _mapper.Map(requestDto, policy);
            _policyRepository.Update(policy);
            return true;
        }

        public bool Delete(Guid id)
        {
            var policy = _policyRepository.GetById(id);
            if (policy == null)
                throw new PolicyNotFoundException("Policy not found.");

            policy.Status = PolicyStatus.DROPPED;
            policy.CancellationDate = DateTime.Now;

            _policyRepository.Update(policy);

            CancelInstallments(id);
            CancelCommissions(id);

            return true;
        }

        // --- Helper Methods ---
        private List<Installment> GenerateInstallments(Policy policy)
        {
            var installments = new List<Installment>();

            if (policy.PremiumType == PremiumType.SINGLE)
            {
                installments.Add(new Installment
                {
                    PolicyId = policy.PolicyId,
                    DueDate = policy.IssueDate,
                    AmountDue = policy.SumAssured,
                    Status = InstallmentStatus.PENDING,
                    PaymentReference = "TEMP-REFERENCE"
                });
            }
            else
            {
                int intervalInMonths = policy.PremiumType switch
                {
                    PremiumType.MONTHLY => 1,
                    PremiumType.QUARTERLY => 3,
                    PremiumType.SEMI_ANNUALLY => 6,
                    PremiumType.ANNUALLY => 12,
                    _ => throw new ArgumentException("Invalid PremiumType.")
                };

                var installmentAmount = policy.SumAssured / (policy.PolicyTerm / intervalInMonths);
                for (int i = 1; i <= policy.PolicyTerm / intervalInMonths; i++)
                {
                    installments.Add(new Installment
                    {
                        PolicyId = policy.PolicyId,
                        DueDate = policy.IssueDate.AddMonths(i * intervalInMonths),
                        AmountDue = installmentAmount,
                        Status = InstallmentStatus.PENDING,
                        PaymentReference = $"TEMP-REF-{i}"
                    });
                }
            }

            return installments;
        }



        private void CalculateCommissions(Policy policy)
        {
            if (policy.AgentId.HasValue && policy.InsuranceScheme != null)
            {
                var commissionAmount = policy.PremiumAmount * policy.InsuranceScheme.RegistrationCommRatio / 100;

                _commissionRepository.Add(new Commission
                {
                    AgentId = policy.AgentId.Value,
                    PolicyNo = policy.PolicyId,
                    Amount = commissionAmount,
                    CommissionType = CommissionType.REGISTRATION
                });
            }
        }

        private void CancelInstallments(Guid policyId)
        {
            var installments = _installmentRepository.GetAll().Where(i => i.PolicyId == policyId).ToList();
            foreach (var installment in installments)
            {
                installment.Status = InstallmentStatus.CANCELLED;
                _installmentRepository.Update(installment);
            }
        }

        private void CancelCommissions(Guid policyId)
        {
            var commissions = _commissionRepository.GetAll().Where(c => c.PolicyNo == policyId).ToList();
            foreach (var commission in commissions)
            {
                commission.Amount = 0;
                _commissionRepository.Update(commission);
            }
        }
        private void AddCommissionForAgent(Policy policy)
        {
            // Ensure agent exists
            if (!policy.AgentId.HasValue || policy.InsuranceScheme == null)
                return;

            // Calculate registration commission
            var commissionAmount = policy.PremiumAmount * policy.InsuranceScheme.RegistrationCommRatio / 100;

            // Create the commission
            var commission = new Commission
            {
                AgentId = policy.AgentId.Value,
                PolicyNo = policy.PolicyId, // Link commission to the policy
                Amount = commissionAmount,
                CommissionType = CommissionType.REGISTRATION,
                IssueDate = DateTime.Now
            };

            // Add the commission to the repository
            _commissionRepository.Add(commission);

            // Save changes
            _context.SaveChanges();
        }
        public List<PolicyResponseDto> GetPoliciesByAgentId(Guid agentId)
        {
            var policies = _policyRepository.GetAll()
                .Include(p => p.Agent)
                .Where(p => p.AgentId == agentId)
                .ToList();

            return _mapper.Map<List<PolicyResponseDto>>(policies);
        }

        public List<PolicyResponseDto> GetPoliciesByCustomerId(Guid customerId)
        {
            var policies = _policyRepository.GetAll()
                .Where(p => p.CustomerId == customerId)
                .ToList();

            return _mapper.Map<List<PolicyResponseDto>>(policies);
        }

        public List<PolicyResponseDto> GetPoliciesBySchemeId(Guid schemeId)
        {
            var policies = _policyRepository.GetAll()
                .Include(p => p.InsuranceScheme)
                .Where(p => p.InsuranceSchemeId == schemeId)
                .ToList();

            return _mapper.Map<List<PolicyResponseDto>>(policies);
        }

        public List<PolicyResponseDto> GetPoliciesByPlanId(Guid planId)
        {
            var policies = _policyRepository.GetAll()
                .Where(p => p.InsuranceScheme.InsurancePlan.PlanId == planId)
                .ToList();

            return _mapper.Map<List<PolicyResponseDto>>(policies);
        }

    }
}