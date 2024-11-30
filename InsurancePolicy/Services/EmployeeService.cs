using AutoMapper;
using InsurancePolicy.DTOs;
using InsurancePolicy.Exceptions.AdminExceptions;
using InsurancePolicy.Exceptions.EmployeeExceptions;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _repository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IRepository<Employee> repository, IRepository<Role> roleRepository, IMapper mapper)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public Guid Add(EmployeeRequestDto employeeRequestDto)
        {
            // Ensure the "Employee" role exists
            var employeeRole = _roleRepository.GetAll().FirstOrDefault(r => r.Name == "Employee");
            if (employeeRole == null)
                throw new Exception("Employee role not found.");

            // Hash the password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(employeeRequestDto.Password);

            // Map EmployeeRequestDto to Employee model
            var employee = _mapper.Map<Employee>(employeeRequestDto);

            // Assign RoleId and hashed password to User
            employee.User.Password = hashedPassword;
            employee.User.RoleId = employeeRole.Id;

            _repository.Add(employee);
            return employee.EmployeeId;
        }

        public EmployeeResponseDto GetById(Guid id)
        {
            var employee = _repository.GetAll()
                .Include(e => e.User) // Include User navigation property
                .FirstOrDefault(e => e.EmployeeId == id);

            if (employee == null)
                throw new EmployeeNotFoundException("No such employee found.");

            return _mapper.Map<EmployeeResponseDto>(employee);
        }

        public List<EmployeeResponseDto> GetAll()
        {
            var employees = _repository.GetAll()
                .Include(e => e.User)
                .ToList();

            if (!employees.Any())
                throw new EmployeesDoesNotExistException("No employees exist.");

            return _mapper.Map<List<EmployeeResponseDto>>(employees);
        }

        public bool Update(EmployeeRequestDto employeeRequestDto)
        {
            if (employeeRequestDto.EmployeeId == null)
                throw new EmployeeNotFoundException("EmployeeId is required for update");

            var existingEmployee = _repository.GetAll()
                .Include(e => e.User)
                .FirstOrDefault(e => e.EmployeeId == employeeRequestDto.EmployeeId.Value);

            if (existingEmployee == null)
                throw new EmployeeNotFoundException("No such employee found.");

            // Map updated values to the existing employee entity
            _mapper.Map(employeeRequestDto, existingEmployee);

            // Hash the password if it's being updated
            if (!string.IsNullOrEmpty(employeeRequestDto.Password))
            {
                existingEmployee.User.Password = BCrypt.Net.BCrypt.HashPassword(employeeRequestDto.Password);
            }

            _repository.Update(existingEmployee);
            return true;
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


    }

}
