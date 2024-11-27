using InsurancePolicy.Exceptions.AgentExceptions;
using InsurancePolicy.Exceptions.CustomerExceptions;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly IRepository<Customer> _repository;
        public CustomerService(IRepository<Customer> repository)
        {
            _repository = repository;
        }
        public Guid Add(Customer customer)
        {
            _repository.Add(customer);
            return customer.CustomerId;
        }

        public bool Delete(Guid id)
        {
            var customer = _repository.GetById(id);
            if (customer != null)
            {
                _repository.Delete(customer);
                return true;
            }
            throw new SchemeNotFoundException("No such customer found to delete");
        }

        public Customer GetById(Guid id)
        {
            var customer = _repository.GetById(id);
            if (customer != null)
                return customer;
            throw new SchemeNotFoundException("No such customer found");
        }

        public List<Customer> GetAll()
        {
            var customers = _repository.GetAll().ToList();
            if (customers.Count == 0)
                throw new SchemeNotFoundException("Customers does not exist!");
            return customers;
        }

        public bool Update(Customer customer)
        {
            var existingCustomer = _repository.GetAll().AsNoTracking().FirstOrDefault(a => a.CustomerId == customer.CustomerId);
            if (existingCustomer != null)
            {
                _repository.Update(customer);
                return true;
            }
            throw new SchemeNotFoundException("No such customer found");
        }
    }
}
