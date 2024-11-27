using InsurancePolicy.Exceptions.AdminExceptions;
using InsurancePolicy.Exceptions.EmployeeExceptions;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class EmployeeService:IEmployeeService
    {
        private readonly IRepository<Employee> _repository;
        public EmployeeService(IRepository<Employee> repository)
        {
            _repository = repository;
        }
        public Guid Add(Employee employee)
        {
            _repository.Add(employee);
            return employee.EmployeeId;
        }

        public bool Delete(Guid id)
        {
            var employee = _repository.GetById(id);
            if (employee != null)
            {
                _repository.Delete(employee);
                return true;
            }
            throw new EmployeeNotFoundException("No such employee found to delete");
        }

        public Employee GetById(Guid id)
        {
            var employee = _repository.GetById(id);
            if (employee != null)
                return employee;
            throw new EmployeeNotFoundException("No such employee found");
        }

        public List<Employee> GetAll()
        {
            var employees = _repository.GetAll().ToList();
            if (employees.Count == 0)
                throw new EmployeesDoesNotExistException("No admins Exist");
            return employees;
        }

        public bool Update(Employee employee)
        {
            var existingEmployee = _repository.GetAll().AsNoTracking().FirstOrDefault(e=>e.EmployeeId == employee.EmployeeId);
            if (existingEmployee != null)
            {
                _repository.Update(employee);
                return true;
            }
            throw new EmployeeNotFoundException("No such role found");
        }
    }
}
