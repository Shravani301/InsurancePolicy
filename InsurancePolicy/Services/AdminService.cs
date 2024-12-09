using AutoMapper;
using InsurancePolicy.DTOs;
using InsurancePolicy.Exceptions.AdminExceptions;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace InsurancePolicy.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<Admin> _repository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public AdminService(
            IRepository<Admin> repository,
            IRepository<Role> roleRepository,
            IMapper mapper)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public Guid Add(AdminRequestDto adminRequestDto)
        {
            // Ensure the "Admin" role exists
            var adminRole = _roleRepository.GetAll().FirstOrDefault(r => r.Name == "Admin");
            if (adminRole == null)
                throw new Exception("Admin role not found."); // Handle this as a custom exception in production

            // Hash the password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(adminRequestDto.Password);

            // Map the AdminRequestDto to the Admin model
            var admin = _mapper.Map<Admin>(adminRequestDto);

            // Set the hashed password and assign the "Admin" role ID
            admin.User.Password = hashedPassword;
            admin.User.RoleId = adminRole.Id; // Assign the RoleId fetched from the database

            // Add the admin to the repository
            _repository.Add(admin);

            // Return the Admin ID
            return admin.AdminId;
        }

        public AdminResponseDto GetById(Guid id)
        {
            var admin = _repository.GetAll().Include(a => a.User)
           .FirstOrDefault(a => a.AdminId == id);
            if (admin == null)
                throw new AdminNotFoundException("No such admin found");

            return _mapper.Map<AdminResponseDto>(admin);
        }
        public AdminResponseDto GetByName(string name)
        {

            // Fetch the admin including the associated user
            var admin = _repository.GetAll()
                .Include(a => a.User)
                .Where(a => a.User.UserName == name).FirstOrDefault();

            if (admin == null)
                throw new AdminsDoesNotExistException($"Admin with username '{admin}' does not exist.");

            // Map the admin entity to AdminResponseDto
            return _mapper.Map<AdminResponseDto>(admin);
        }
        public List<AdminResponseDto> GetAll()
        {
            var admins = _repository.GetAll().Include(a => a.User).ToList();
            if (!admins.Any())
                throw new AdminsDoesNotExistException("No admins exist");

            return _mapper.Map<List<AdminResponseDto>>(admins);
        }

        public bool Update(AdminRequestDto adminRequestDto)
        {
            if (adminRequestDto.AdminId == null)
                throw new AdminNotFoundException("AdminId is required for update");

            var existingAdmin = _repository.GetAll()
                .Include(a => a.User)
                .FirstOrDefault(a => a.AdminId == adminRequestDto.AdminId.Value);

            if (existingAdmin == null)
                throw new AdminNotFoundException("No such admin found");

            // Map updated values to the existing admin entity
            _mapper.Map(adminRequestDto, existingAdmin);

            // Fetch the Admin role
            var adminRole = _roleRepository.GetAll().FirstOrDefault(r => r.Name == "Admin");
            if (adminRole == null)
                throw new Exception("Admin role not found.");

            // Update the User entity
            if (!string.IsNullOrEmpty(adminRequestDto.Password))
            {
                existingAdmin.User.Password = BCrypt.Net.BCrypt.HashPassword(adminRequestDto.Password);
            }
            existingAdmin.User.RoleId = adminRole.Id; 

            _repository.Update(existingAdmin);
            return true;
        }
    }
}
