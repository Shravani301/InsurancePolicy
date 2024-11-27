using InsurancePolicy.Exceptions.PaymentExceptions;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class PaymentService:IPaymentService
    {
        private readonly IRepository<Payment> _repository;
        public PaymentService(IRepository<Payment> repository)
        {
            _repository = repository;
        }
        public Guid Add(Payment payment)
        {
            _repository.Add(payment);
            return payment.PaymentId;
        }       

        public Payment GetById(Guid id)
        {
            var payment = _repository.GetById(id);
            if (payment != null)
                return payment;
            throw new PaymentNotFoundException("No such admin found");
        }

        public List<Payment> GetAll()
        {
            var payments = _repository.GetAll().ToList();
            if (payments.Count == 0)
                throw new PaymentsDoesNotExistException("No admins Exist");
            return payments;
        }

        
    }
}
