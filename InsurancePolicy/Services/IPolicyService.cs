using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface IPolicyService
    {
        public List<Policy> GetAll();
        public Policy GetById(Guid id);
        public Guid Add(Policy policy);
        public bool Update(Policy policy);
        public bool Delete(Guid id);
    }
}
