using InsurancePolicy.DTOs;
using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface IPaymentService
    {
        public List<PaymentResponseDto> GetAllPayments();
        public List<PaymentResponseDto> GetPaymentsByPolicy(Guid id);
        public Guid AddPayment(PaymentRequestDto payment);
    }
}
