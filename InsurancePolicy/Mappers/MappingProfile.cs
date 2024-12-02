using AutoMapper;
using InsurancePolicy.DTOs;
using InsurancePolicy.enums;
using InsurancePolicy.Models;

namespace InsurancePolicy.Mappers
{
    public class MappingProfile : Profile
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
        .ForMember(dest => dest.Address, opt => opt.MapFrom((src, dest) =>
        {
            if (dest.Address == null)
            {
                return new Address
                {
                    HouseNo = src.HouseNo, // Map individual address fields
                    Apartment = src.Apartment,
                    Pincode = src.Pincode,
                    City = new City
                    {
                        CityId = Guid.NewGuid(), // Generate new City ID for Add operations
                        CityName = src.City,
                        State = new State
                        {
                            StateId = Guid.NewGuid(), // Generate new State ID for Add operations
                            StateName = src.State
                        }
                    }
                };
            }

            // Update existing address details
            dest.Address.HouseNo = src.HouseNo;
            dest.Address.Apartment = src.Apartment;
            dest.Address.Pincode = src.Pincode;

            if (dest.Address.City != null)
            {
                dest.Address.City.CityName = src.City;
                if (dest.Address.City.State != null)
                {
                    dest.Address.City.State.StateName = src.State;
                }
            }

            return dest.Address;
        }))
        .ForMember(dest => dest.AgentId, opt => opt.Condition(src => src.AgentId.HasValue)) // Allow null AgentId
        .ForMember(dest => dest.CustomerId, opt => opt.Condition(src => src.CustomerId.HasValue)); // Map CustomerId only if provided

            CreateMap<Customer, CustomerResponseDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName)) // Map UserName
                .ForMember(dest => dest.TotalPolicies, opt => opt.MapFrom(src => src.Policies != null ? src.Policies.Count : 0)) // Map TotalPolicies
                .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.Agent != null
                    ? $"{src.Agent.AgentFirstName} {src.Agent.AgentLastName}" // Combine Agent's first and last name
                    : null)) // If no Agent, set null
                .ForMember(dest => dest.HouseNo, opt => opt.MapFrom(src => src.Address != null ? src.Address.HouseNo : null))
                .ForMember(dest => dest.Apartment, opt => opt.MapFrom(src => src.Address != null ? src.Address.Apartment : null))
                .ForMember(dest => dest.Pincode, opt => opt.MapFrom(src => src.Address != null ? src.Address.Pincode : 0))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address != null && src.Address.City != null ? src.Address.City.CityName : null))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.Address != null && src.Address.City != null && src.Address.City.State != null
                    ? src.Address.City.State.StateName
                    : null));

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


            CreateMap<InsurancePlanRequestDto, InsurancePlan>()
    .ForMember(dest => dest.PlanId, opt => opt.Condition(src => src.PlanId.HasValue)) // Map PlanId only if provided
    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status)) // Map Status directly
    .ForMember(dest => dest.Schemes, opt => opt.Ignore()); // Ignore Schemes during mapping from request DTO

            CreateMap<InsurancePlan, InsurancePlanResponseDto>()
    .ForMember(dest => dest.SchemeNames, opt => opt.MapFrom(src => src.Schemes != null
        ? src.Schemes.Select(s => s.SchemeName).ToList()
        : null)); // Map only scheme names to SchemeNames



            CreateMap<InsuranceSchemeRequestDto, InsuranceScheme>()
    .ForMember(dest => dest.SchemeId, opt => opt.Condition(src => src.SchemeId.HasValue));

            CreateMap<InsuranceScheme, InsuranceSchemeResponseDto>()
                .ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.InsurancePlan != null ? src.InsurancePlan.PlanName : null))
                .ForMember(dest => dest.PoliciesCount, opt => opt.MapFrom(src => src.Policies != null ? src.Policies.Count : 0));

            CreateMap<TaxSettingsRequestDto, TaxSettings>()
                .ForMember(dest => dest.TaxId, opt => opt.Condition(src => src.TaxId != Guid.Empty)); // Map TaxId only if it exists

            CreateMap<TaxSettings, TaxSettingsResponseDto>();
            CreateMap<NomineeRequestDto, Nominee>()
           .ForMember(dest => dest.NomineeId, opt => opt.Condition(src => src.NomineeId.HasValue))
           .ForMember(dest => dest.Relationship, opt => opt.MapFrom(src => src.Relationship.ToString()));

            CreateMap<Nominee, NomineeResponseDto>()
                .ForMember(dest => dest.Relationship, opt => opt.MapFrom(src => src.Relationship.ToString()));

            CreateMap<InstallmentRequestDto, Installment>()
            .ForMember(dest => dest.InstallmentId, opt => opt.Condition(src => src.InstallmentId.HasValue));

            CreateMap<Installment, InstallmentResponseDto>()
                .ForMember(dest => dest.PolicyName, opt => opt.MapFrom(src => src.InsurancePolicy != null ? src.InsurancePolicy.InsuranceScheme.SchemeName : "N/A"));
            CreateMap<PaymentRequestDto, Payment>();
            CreateMap<Payment, PaymentResponseDto>()
                .ForMember(dest => dest.PolicyName, opt => opt.MapFrom(src => src.Policy.InsuranceScheme.SchemeName));

            // Commission mappings
            CreateMap<CommissionRequestDto, Commission>();

            CreateMap<Commission, CommissionResponseDto>()
                .ForMember(dest => dest.CommissionType, opt => opt.MapFrom(src => src.CommissionType.ToString())) // Map enum to string
                .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.Agent != null ? src.Agent.AgentFirstName : null))
                .ForMember(dest => dest.PolicyName, opt => opt.MapFrom(src => src.PolicyAccount != null ? src.PolicyAccount.InsuranceScheme.SchemeName : null));

            // Claim mappings
            CreateMap<ClaimRequestDto, Claim>();
            CreateMap<Claim, ClaimResponseDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.PolicyName, opt => opt.MapFrom(src => src.Policy != null ? src.Policy.InsuranceScheme.SchemeName : null))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.CustomerFirstName : null));

            // PolicyRequestDto to Policy
            // Map InsuranceSettingsRequestDto to InsuranceSettings
            CreateMap<InsuranceSettingsRequestDto, InsuranceSettings>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignore Id since it is generated by the database

            // Map InsuranceSettings to InsuranceSettingsResponseDto
            CreateMap<InsuranceSettings, InsuranceSettingsResponseDto>();

            // Map PolicyRequestDto to Policy
            CreateMap<PolicyRequestDto, Policy>()
                .ForMember(dest => dest.PolicyId, opt => opt.Ignore()) // Set by the database
                .ForMember(dest => dest.Installments, opt => opt.Ignore()) // Installments handled separately
                .ForMember(dest => dest.Nominees, opt => opt.Ignore()) // Added separately
                .ForMember(dest => dest.Payments, opt => opt.Ignore()) // Generated separately
                .ForMember(dest => dest.TaxSettings, opt => opt.Ignore()) // Set in service
                .ForMember(dest => dest.InsuranceSettings, opt => opt.Ignore()); // Set in service

            // Policy to PolicyResponseDto
            CreateMap<Policy, PolicyResponseDto>()
                .ForMember(dest => dest.InsuranceSchemeName, opt => opt.MapFrom(src => src.InsuranceScheme.SchemeName))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.CustomerFirstName))
                .ForMember(dest => dest.PolicyStatus, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.AgentName, opt => opt.MapFrom(src => src.Agent.AgentFirstName))
                .ForMember(dest => dest.Tax, opt => opt.MapFrom(src => src.TaxSettings.TaxPercentage))
                .ForMember(dest => dest.Installments, opt => opt.MapFrom(src => src.Installments))
                .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments));

            CreateMap<InstallmentRequestDto, Installment>()
     .ForMember(dest => dest.InstallmentId, opt => opt.Condition(src => src.InstallmentId.HasValue))
     .ForMember(dest => dest.PolicyNo, opt => opt.Ignore()); // Set explicitly in service

            // Installment to InstallmentResponseDto
            CreateMap<Installment, InstallmentResponseDto>()
                .ForMember(dest => dest.PolicyName, opt => opt.MapFrom(src => src.InsurancePolicy.InsuranceScheme.SchemeName));

            // PaymentRequestDto to Payment
            CreateMap<PaymentRequestDto, Payment>()
                .ForMember(dest => dest.PolicyId, opt => opt.Ignore()); // Set in service

            // Payment to PaymentResponseDto
            CreateMap<Payment, PaymentResponseDto>()
                .ForMember(dest => dest.PolicyName, opt => opt.MapFrom(src => src.Policy.InsuranceScheme.SchemeName));

            // Other mappings (e.g., Agent, Employee)
            CreateMap<Agent, AgentInfoDto>();
            CreateMap<Employee, EmployeeInfoDto>();

        }
    }
}
