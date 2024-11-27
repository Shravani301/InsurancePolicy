using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface ICustomerService
    {
        public List<Customer> GetAll();
        public Customer GetById(Guid id);
        public Guid Add(Customer customer);
        public bool Update(Customer customer);
        public bool Delete(Guid id);
    }
}
