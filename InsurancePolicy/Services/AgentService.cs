using InsurancePolicy.Exceptions.AgentExceptions;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class AgentService:IAgentService
    {
        private readonly IRepository<Agent> _repository;
        public AgentService(IRepository<Agent> repository)
        {
            _repository = repository;
        }
        public Guid Add(Agent agent)
        {
            _repository.Add(agent);
            return agent.AgentId;
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

        public Agent GetById(Guid id)
        {
            var agent = _repository.GetById(id);
            if (agent != null)
                return agent;
            throw new AgentNotFoundException("No such agent found");
        }

        public List<Agent> GetAll()
        {
            var agents = _repository.GetAll().ToList();
            if (agents.Count == 0)
                throw new AgentsDoesNotExistException("No agents Exist");
            return agents;
        }

        public bool Update(Agent agent)
        {
            var existingAgent = _repository.GetAll().AsNoTracking().FirstOrDefault(a => a.AgentId == agent.AgentId);
            if (existingAgent != null)
            {
                _repository.Update(agent);
                return true;
            }
            throw new AgentNotFoundException("No such role found");
        }
    }
}
