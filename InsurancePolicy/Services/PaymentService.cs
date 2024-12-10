using AutoMapper;
using InsurancePolicy.DTOs;
using InsurancePolicy.Helpers;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment> _paymentRepository;
        private readonly IRepository<Policy> _policyRepository;
        private readonly IMapper _mapper;

        public PaymentService(
            IRepository<Payment> paymentRepository,
            IRepository<Policy> policyRepository,
            IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _policyRepository = policyRepository;
            _mapper = mapper;
        }

        public Guid AddPayment(PaymentRequestDto paymentDto)
        {
            // Validate associated policy
            var policy = _policyRepository.GetById(paymentDto.PolicyId);
            if (policy == null)
                throw new ArgumentException("Invalid Policy ID.");

            // Map DTO to Entity
            var payment = _mapper.Map<Payment>(paymentDto);

            // Calculate total payment
            payment.TotalPayment = payment.AmountPaid + payment.Tax;

            // Save payment
            _paymentRepository.Add(payment);

            return payment.PaymentId;
        }

        public List<PaymentResponseDto> GetPaymentsByPolicy(Guid policyId)
        {
            var payments = _paymentRepository.GetAll()
                .Where(p => p.PolicyId == policyId)
                .ToList();

            return _mapper.Map<List<PaymentResponseDto>>(payments);
        }
        public PageList<PaymentResponseDto> GetAllPaginated(PageParameters pageParameters)
        {
            var paymentsQuery = _paymentRepository.GetAll()
                .Include(p => p.Policy)
                .AsQueryable();

            var paginatedPayments = PageList<PaymentResponseDto>.ToPagedList(
                _mapper.Map<List<PaymentResponseDto>>(paymentsQuery.ToList()),
                pageParameters.PageNumber,
                pageParameters.PageSize
            );

            return paginatedPayments;
        }

        public List<PaymentResponseDto> GetAllPayments()
        {
            var payments = _paymentRepository.GetAll().Include(p=> p.Policy).ToList();
            return _mapper.Map<List<PaymentResponseDto>>(payments);
        }
    }
}
