using InsurancePolicy.DTOs;
using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface IEmployeeService
    {
        public List<EmployeeResponseDto> GetAll();
        public EmployeeResponseDto GetById(Guid id);
        public Guid Add(EmployeeRequestDto employee);
        public bool Update(EmployeeRequestDto employee);
        public bool Delete(Guid id);
    }
}
