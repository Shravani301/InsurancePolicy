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
        private readonly IRepository<Payment>_paymentRepository;
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
            IRepository<Payment> paymentRepository,
            IMapper mapper,
            AppDbContext context)
        {
            _policyRepository = policyRepository;
            _installmentRepository = installmentRepository;
            _commissionRepository = commissionRepository;
            _nomineeRepository = nomineeRepository;
            _insuranceSchemeRepository = insuranceSchemeRepository;
            _taxSettingsRepository = taxSettingsRepository;
            _insuranceSettingsRepository = insuranceSettingsRepository;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _context = context;
        }

        public List<PolicyResponseDto> GetAll()
        {
            var policies = _policyRepository.GetAll()
                .Include(p => p.InsuranceScheme)
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
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                // Map the request DTO to the Policy entity
                var policy = _mapper.Map<Policy>(requestDto);

                // Validate InsuranceSettingId
                if (requestDto.InsuranceSettingId.HasValue)
                {
                    var insuranceSetting = _insuranceSettingsRepository.GetById(requestDto.InsuranceSettingId.Value);
                    if (insuranceSetting == null)
                    {
                        throw new InvalidOperationException($"Invalid InsuranceSettingId: {requestDto.InsuranceSettingId}");
                    }
                    policy.InsuranceSettings = insuranceSetting;
                }

                // Validate TaxId
                if (requestDto.TaxId.HasValue)
                {
                    var taxSetting = _taxSettingsRepository.GetById(requestDto.TaxId.Value);
                    if (taxSetting == null)
                    {
                        throw new InvalidOperationException($"Invalid TaxId: {requestDto.TaxId}");
                    }
                    policy.TaxSettings = taxSetting;
                }

                // Set default properties
                policy.Status = PolicyStatus.PENDING;
                policy.IssueDate = DateTime.Now;

                // Save Policy first to generate PolicyId
                _policyRepository.Add(policy);
                _context.SaveChanges();

                Console.WriteLine($"Policy saved with PolicyId: {policy.PolicyId}");

                // Generate Installments (if applicable)
                var installments = GenerateInstallments(policy);
                policy.Installments = installments;

                // Add Commissions if AgentId exists
                if (policy.AgentId.HasValue)
                {
                    CalculateCommissions(policy);
                }

                // Commit transaction
                _context.SaveChanges();
                transaction.Commit();

                return policy.PolicyId;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Error adding policy: {ex.Message}");
                throw;
            }
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
            double taxPercentage = policy.TaxSettings?.TaxPercentage ?? 0;

            if (policy.PremiumType == PremiumType.SINGLE)
            {
                var taxAmount = policy.SumAssured * (taxPercentage / 100);
                installments.Add(new Installment
                {
                    PolicyNo = policy.PolicyId, // Directly associate with the PolicyId
                    DueDate = policy.IssueDate,
                    AmountDue = policy.SumAssured + taxAmount,
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

                var baseInstallmentAmount = policy.SumAssured / (policy.PolicyTerm / intervalInMonths);
                for (int i = 1; i <= policy.PolicyTerm / intervalInMonths; i++)
                {
                    var taxAmount = baseInstallmentAmount * (taxPercentage / 100);
                    installments.Add(new Installment
                    {
                        PolicyNo = policy.PolicyId, // Directly associate with the PolicyId
                        DueDate = policy.IssueDate.AddMonths(i * intervalInMonths),
                        AmountDue = baseInstallmentAmount + taxAmount,
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

                var commission = new Commission
                {
                    AgentId = policy.AgentId.Value,
                    PolicyNo = policy.PolicyId,
                    Amount = commissionAmount,
                    CommissionType = CommissionType.REGISTRATION,
                    IssueDate = DateTime.Now
                };

                _commissionRepository.Add(commission);
            }
        }


        private void CancelInstallments(Guid policyId)
        {
            var installments = _installmentRepository.GetAll().Where(i => i.PolicyNo == policyId).ToList();
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
        private void GenerateFirstPayment(Policy policy)
        {
            if (policy.Installments.Any())
            {
                var firstInstallment = policy.Installments.First();

                var payment = new Payment
                {
                    PolicyId = policy.PolicyId,
                    AmountPaid = firstInstallment.AmountDue,
                    Tax = firstInstallment.AmountDue * 0.1, // Example tax calculation
                    TotalPayment = firstInstallment.AmountDue,
                    PaymentDate = DateTime.Now,
                    paymentType = PaymentType.CREDIT // Default payment type
                };

                _paymentRepository.Add(payment);

                // Update installment status
                firstInstallment.Status = InstallmentStatus.PAID;
                _installmentRepository.Update(firstInstallment);
            }
        }
    }
}
