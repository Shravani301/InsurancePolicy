using InsurancePolicy.DTOs;
using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface ICustomerService
    {
        public List<CustomerResponseDto> GetAll();
        public CustomerResponseDto GetById(Guid id);
        public Guid Add(CustomerRequestDto customer);
        public bool Update(CustomerRequestDto customer);
        public bool Delete(Guid id);
    }
}
