using AutoMapper;
using InsurancePolicy.DTOs;
using InsurancePolicy.Exceptions.CustomerExceptions;
using InsurancePolicy.Helpers;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _repository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public CustomerService(IRepository<Customer> repository, IRepository<Role> roleRepository, IMapper mapper)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public Guid Add(CustomerRequestDto customerRequestDto)
        {
            // Ensure the "Customer" role exists
            var customerRole = _roleRepository.GetAll().FirstOrDefault(r => r.Name == "Customer");
            if (customerRole == null)
                throw new Exception("Customer role not found.");
            var userNameCheck = _roleRepository
                .GetAll()
                .Include(u => u.Users)
                .FirstOrDefault(u => u.Users.Any(user => user.UserName == customerRequestDto.UserName));

            if (userNameCheck != null)
            {
                throw new BadHttpRequestException("Username already exists.", StatusCodes.Status409Conflict);
            }

            // Hash the password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(customerRequestDto.Password);

            // Map CustomerRequestDto to Customer model
            var customer = _mapper.Map<Customer>(customerRequestDto);

            // Assign RoleId and hashed password to User
            customer.User.Password = hashedPassword;
            customer.User.RoleId = customerRole.Id;

            // Handle Address, City, and State
            if (customer.Address == null)
            {
                customer.Address = new Address
                {
                    HouseNo = customerRequestDto.HouseNo,
                    Apartment = customerRequestDto.Apartment,
                    Pincode = customerRequestDto.Pincode,
                    City = new City
                    {
                        CityName = customerRequestDto.City,
                        State = new State
                        {
                            StateName = customerRequestDto.State
                        }
                    }
                };
            }

            _repository.Add(customer);
            return customer.CustomerId;
        }

        public CustomerResponseDto GetById(Guid id)
        {
            var customer = _repository.GetAll()
                .Include(c => c.User)
                .Include(c => c.Agent)
                .Include(c => c.Policies)
                .Include(a => a.Address)
                    .ThenInclude(ad => ad.City)
                        .ThenInclude(city => city.State)
                .FirstOrDefault(c => c.CustomerId == id);

            if (customer == null)
                throw new CustomerNotFoundException("No such customer found.");

            return _mapper.Map<CustomerResponseDto>(customer);
        }

        public List<CustomerResponseDto> GetAll()
        {
            var customers = _repository.GetAll()
                .Include(c => c.User)
                .Include(a => a.Agent)
                .Include(a => a.Address)
                    .ThenInclude(ad => ad.City)
                        .ThenInclude(city => city.State)
                .Include(c => c.Policies)
                .ToList();

            if (!customers.Any())
                throw new CustomersDoesNotExistException("No customers exist.");

            return _mapper.Map<List<CustomerResponseDto>>(customers);
        }
        public PageList<CustomerResponseDto> GetAllPaginated(PageParameters pageParameters)
        {
            var customers = _repository.GetAll().ToList();
            var pagedCustomers = PageList<CustomerResponseDto>.ToPagedList(
                _mapper.Map<List<CustomerResponseDto>>(customers),
                pageParameters.PageNumber,
                pageParameters.PageSize
            );
            return pagedCustomers;
        }

        public bool Update(CustomerRequestDto customerRequestDto)
        {
            if (customerRequestDto.CustomerId == null)
                throw new CustomerNotFoundException("CustomerId is required for update");

            var existingCustomer = _repository.GetAll()
                .Include(c => c.User)
                .Include(a => a.Address)
                    .ThenInclude(ad => ad.City)
                        .ThenInclude(city => city.State)
                .FirstOrDefault(c => c.CustomerId == customerRequestDto.CustomerId.Value);

            if (existingCustomer == null)
                throw new CustomerNotFoundException("No such customer found.");

            // Update basic fields
            _mapper.Map(customerRequestDto, existingCustomer);

            // Update Address, City, and State
            if (existingCustomer.Address == null)
            {
                existingCustomer.Address = new Address
                {
                    HouseNo = customerRequestDto.HouseNo,
                    Apartment = customerRequestDto.Apartment,
                    Pincode = customerRequestDto.Pincode,
                    City = new City
                    {
                        CityName = customerRequestDto.City,
                        State = new State
                        {
                            StateName = customerRequestDto.State
                        }
                    }
                };
            }
            else
            {
                existingCustomer.Address.HouseNo = customerRequestDto.HouseNo;
                existingCustomer.Address.Apartment = customerRequestDto.Apartment;
                existingCustomer.Address.Pincode = customerRequestDto.Pincode;

                if (existingCustomer.Address.City == null)
                {
                    existingCustomer.Address.City = new City
                    {
                        CityName = customerRequestDto.City,
                        State = new State
                        {
                            StateName = customerRequestDto.State
                        }
                    };
                }
                else
                {
                    existingCustomer.Address.City.CityName = customerRequestDto.City;
                    if (existingCustomer.Address.City.State == null)
                    {
                        existingCustomer.Address.City.State = new State
                        {
                            StateName = customerRequestDto.State
                        };
                    }
                    else
                    {
                        existingCustomer.Address.City.State.StateName = customerRequestDto.State;
                    }
                }
            }

            // Hash the password if it's being updated
            if (!string.IsNullOrEmpty(customerRequestDto.Password))
            {
                existingCustomer.User.Password = BCrypt.Net.BCrypt.HashPassword(customerRequestDto.Password);
            }

            _repository.Update(existingCustomer);
            return true;
        }

        public bool Delete(Guid id)
        {
            var customer = _repository.GetById(id);
            if (customer != null)
            {
                _repository.Delete(customer);
                return true;
            }
            throw new CustomerNotFoundException("No such customer found to delete");
        }
    }
}
