using AutoMapper;
using InsurancePolicy.DTOs;
using InsurancePolicy.Models;

namespace InsurancePolicy.Mappers
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        {
            CreateMap<AdminRequestDto, Admin>()
    .ForMember(dest => dest.User, opt => opt.MapFrom((src, dest) =>
    {
        if (dest.User == null)
        {
            // Create a new User if none exists (e.g., during Add operation)
            return new User
            {
                UserName = src.UserName,
                Password = src.Password, // Leave encryption to service layer
                RoleId = Guid.Empty // Assign RoleId in service
            };
        }

        // Update existing User (e.g., during Update operation)
        dest.User.UserName = src.UserName;
        if (!string.IsNullOrEmpty(src.Password))
        {
            dest.User.Password = src.Password; // Leave encryption to service layer
        }
        return dest.User;
    }))
    .ForMember(dest => dest.AdminId, opt => opt.Condition(src => src.AdminId.HasValue)); // Map AdminId only if provided

            // Mapping from Admin to AdminResponseDto
            CreateMap<Admin, AdminResponseDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName)); // Map UserName

            // Mapping from AgentRequestDto to Agent
            CreateMap<AgentRequestDto, Agent>()
        .ForMember(dest => dest.User, opt => opt.MapFrom((src, dest) =>
        {
            if (dest.User == null)
            {
                // Create a new User for Add operations
                return new User
                {
                    UserName = src.UserName,
                    Password = src.Password, // Encryption handled in the service layer
                    RoleId = Guid.Empty // Assign RoleId in the service layer
                };
            }

            // Update existing User for Update operations
            dest.User.UserName = src.UserName;
            if (!string.IsNullOrEmpty(src.Password))
            {
                dest.User.Password = src.Password; // Encryption handled in the service layer
            }
            return dest.User;
        }))
        .ForMember(dest => dest.AgentId, opt => opt.Condition(src => src.AgentId.HasValue)); // Map AgentId only if provided

            CreateMap<Agent, AgentResponseDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.TotalCustomers, opt => opt.MapFrom(src => src.Customers != null ? src.Customers.Count : 0));

            // Mapping from CustomerRequestDto to Customer
            CreateMap<CustomerRequestDto, Customer>()
                .ForMember(dest => dest.User, opt => opt.MapFrom((src, dest) =>
                {
                    if (dest.User == null)
                    {
                        // Create a new User for Add operations
                        return new User
                        {
                            UserName = src.UserName,
                            Password = src.Password, // Encrypt in the service layer
                            RoleId = Guid.Empty // Assign RoleId in the service layer
                        };
                    }

                    // Update existing User for Update operations
                    dest.User.UserName = src.UserName;
                    if (!string.IsNullOrEmpty(src.Password))
                    {
                        dest.User.Password = src.Password; // Encrypt in the service layer
                    }

                    return dest.User;
                }))
                .ForMember(dest => dest.AgentId, opt => opt.Condition(src => src.AgentId.HasValue)) // Allow null AgentId
                .ForMember(dest => dest.CustomerId, opt => opt.Condition(src => src.CustomerId.HasValue)); // Map CustomerId only if provided

            // Mapping from Customer to CustomerResponseDto
            CreateMap<Customer, CustomerResponseDto>()
    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName)) // Map UserName
    .ForMember(dest => dest.TotalPolicies, opt => opt.MapFrom(src => src.Policies != null ? src.Policies.Count : 0)) // Map TotalPolicies
    .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.Agent != null
        ? $"{src.Agent.AgentFirstName} {src.Agent.AgentLastName}" // Combine Agent's first and last name
        : null)); // If no Agent, set null



            CreateMap<EmployeeRequestDto, Employee>()
        .ForMember(dest => dest.User, opt => opt.MapFrom((src, dest) =>
        {
            if (dest.User == null)
            {
                // Create a new User for Add operations
                return new User
                {
                    UserName = src.UserName,
                    Password = src.Password, // Encrypt in the service layer
                    RoleId = Guid.Empty // Assign RoleId in the service layer
                };
            }

            // For updates, update the existing User
            dest.User.UserName = src.UserName;
            if (!string.IsNullOrEmpty(src.Password))
            {
                dest.User.Password = src.Password; // Encrypt in the service layer
            }

            return dest.User;
        }))
        .ForMember(dest => dest.EmployeeId, opt => opt.Condition(src => src.EmployeeId.HasValue)); // Map EmployeeId only if provided

            CreateMap<Employee, EmployeeResponseDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));














            // Other mappings (e.g., Agent, Employee)
            CreateMap<Agent, AgentInfoDto>();
            CreateMap<Employee, EmployeeInfoDto>();

        }
    }
}
