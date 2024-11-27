using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface IPaymentService
    {
        public List<Payment> GetAll();
        public Payment GetById(Guid id);
        public Guid Add(Payment payment);
    }
}
