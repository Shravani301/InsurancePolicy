using AutoMapper;
using InsurancePolicy.DTOs;
using InsurancePolicy.Exceptions.AgentExceptions;
using InsurancePolicy.Exceptions.PlanExceptions;
using InsurancePolicy.Helpers;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class AgentService : IAgentService
    {
        private readonly IRepository<Agent> _repository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public AgentService(
            IRepository<Agent> repository,
            IRepository<Role> roleRepository,
            IMapper mapper)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public PageList<AgentResponseDto> GetAllPaginated(PageParameters pageParameters)
        {
            var agents = _repository.GetAll().Include(c=>c.Customers).ToList();
            var paginatedAgents = PageList<AgentResponseDto>.ToPagedList(
                _mapper.Map<List<AgentResponseDto>>(agents),
                pageParameters.PageNumber,
                pageParameters.PageSize
            );

            return paginatedAgents;
        }
        public Guid Add(AgentRequestDto agentRequestDto)
        {
            // Ensure the "Agent" role exists
            var agentRole = _roleRepository.GetAll().FirstOrDefault(r => r.Name == "Agent");
            if (agentRole == null)
                throw new Exception("Agent role not found.");

            // Hash the password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(agentRequestDto.Password);

            // Map the AgentRequestDto to the Agent model
            var agent = _mapper.Map<Agent>(agentRequestDto);

            // Set the hashed password and assign the "Agent" role ID
            agent.User.Password = hashedPassword;
            agent.User.RoleId = agentRole.Id;

            // Add the agent to the repository
            _repository.Add(agent);

            return agent.AgentId;
        }

        public AgentResponseDto GetById(Guid id)
        {
            var agent = _repository.GetAll()
                .Include(a => a.User) // Include User navigation property
                .Include(a => a.Customers) // Include Customers navigation property
                .FirstOrDefault(a => a.AgentId == id);

            if (agent == null)
                throw new AgentNotFoundException("No such agent found");

            return _mapper.Map<AgentResponseDto>(agent);
        }

        public List<AgentResponseDto> GetAll()
        {
            var agents = _repository.GetAll()
                .Include(a => a.User) // Include User navigation property
                .Include(a => a.Customers) // Include Customers navigation property
                .ToList();

            if (!agents.Any())
                throw new AgentsDoesNotExistException("No agents exist");

            return _mapper.Map<List<AgentResponseDto>>(agents);
        }

        public bool Update(AgentRequestDto agentRequestDto)
        {
            if (agentRequestDto.AgentId == null)
                throw new AgentNotFoundException("AgentId is required for update");

            var existingAgent = _repository.GetAll()
                .Include(a => a.User)
                .Include(a => a.Customers)
                .FirstOrDefault(a => a.AgentId == agentRequestDto.AgentId.Value);

            if (existingAgent == null)
                throw new AgentNotFoundException("No such agent found");

            // Map updated values to the existing agent entity
            _mapper.Map(agentRequestDto, existingAgent);

            // Hash the password if it's being updated
            if (!string.IsNullOrEmpty(agentRequestDto.Password))
            {
                existingAgent.User.Password = BCrypt.Net.BCrypt.HashPassword(agentRequestDto.Password);
            }

            // Preserve the existing RoleId
            var agentRole = _roleRepository.GetAll().FirstOrDefault(r => r.Name == "Agent");
            if (agentRole != null)
            {
                existingAgent.User.RoleId = agentRole.Id;
            }

            _repository.Update(existingAgent);
            return true;
        }
        public void Activate(Guid id)
        {
            var agent = _repository.GetById(id);
            if (agent == null)
                throw new AgentNotFoundException("No such agent found to activate");

            _repository.Activate(agent);
        }
        public List<AgentResponseDto> GetAgentsByCustomerId(Guid customerId)
        {
            // Find agents associated with the given customerId
            var agents = _repository.GetAll()
                .Include(a => a.Customers) // Include Customers navigation property
                .Where(a => a.Customers.Any(c => c.CustomerId == customerId))
                .ToList();

            if (!agents.Any())
                throw new AgentNotFoundException("No agents found for the specified customer.");

            return _mapper.Map<List<AgentResponseDto>>(agents);
        }
        public PageList<AgentResponseDto> GetAgentsByCustomerId(Guid customerId, PageParameters pageParameters)
        {
            // Fetch agents associated with the given customerId
            var agents = _repository.GetAll()
                .Include(a => a.Customers) // Include Customers navigation property
                .Where(a => a.Customers.Any(c => c.CustomerId == customerId))
                .ToList();

            if (!agents.Any())
                throw new AgentNotFoundException("No agents found for the specified customer.");

            // Apply pagination
            var paginatedAgents = PageList<AgentResponseDto>.ToPagedList(
                _mapper.Map<List<AgentResponseDto>>(agents),
                pageParameters.PageNumber,
                pageParameters.PageSize
            );

            return paginatedAgents;
        }

        public bool Delete(Guid id)
        {
            var agent = _repository.GetById(id);
            if (agent != null)
            {
                _repository.Delete(agent);
                return true;
            }
            throw new AgentNotFoundException("No such agent found to delete");
        }

    }
}
