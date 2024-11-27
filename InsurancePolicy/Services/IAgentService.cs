using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface IAgentService
    {
        public List<Agent> GetAll();
        public Agent GetById(Guid id);
        public Guid Add(Agent agent);
        public bool Update(Agent agent);
        public bool Delete(Guid id);
    }
}
