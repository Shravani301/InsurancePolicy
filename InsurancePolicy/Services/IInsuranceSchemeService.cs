using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface IInsuranceSchemeService
    {
        public List<InsuranceScheme> GetAll();
        public InsuranceScheme GetById(Guid id);
        public Guid Add(InsuranceScheme scheme);
        public bool Update(InsuranceScheme scheme);
        public bool Delete(Guid id);
    }
}
