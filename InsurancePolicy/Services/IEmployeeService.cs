using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface IEmployeeService
    {
        public List<Employee> GetAll();
        public Employee GetById(Guid id);
        public Guid Add(Employee employee);
        public bool Update(Employee employee);
        public bool Delete(Guid id);
    }
}
