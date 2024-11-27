using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface IInsurancePlanService
    {
        public List<InsurancePlan> GetAll();
        public InsurancePlan GetById(Guid id);
        public Guid Add(InsurancePlan plan);
        public bool Update(InsurancePlan plan);
        public bool Delete(Guid id);
    }
}
