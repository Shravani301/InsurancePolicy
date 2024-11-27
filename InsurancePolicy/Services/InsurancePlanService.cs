using InsurancePolicy.Exceptions.PlanExceptions;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class InsurancePlanService:IInsurancePlanService
    {
        private readonly IRepository<InsurancePlan> _repository;
        public InsurancePlanService(IRepository<InsurancePlan> repository)
        {
            _repository = repository;
        }
        public Guid Add(InsurancePlan plan)
        {
            _repository.Add(plan);
            return plan.PlanId;
        }

        public bool Delete(Guid id)
        {
            var plan = _repository.GetById(id);
            if (plan != null)
            {
                _repository.Delete(plan);
                return true;
            }
            throw new PlanNotFoundException("No such plan found to delete");
        }

        public InsurancePlan GetById(Guid id)
        {
            var plan = _repository.GetById(id);
            if (plan != null)
                return plan;
            throw new PlanNotFoundException("No such plan found");
        }

        public List<InsurancePlan> GetAll()
        {
            var plans = _repository.GetAll().ToList();
            if (plans.Count == 0)
                throw new PlansDoesNotExistException("No plans Exist");
            return plans;
        }

        public bool Update( InsurancePlan plan)
        {
            var existingPlan = _repository.GetAll().AsNoTracking().FirstOrDefault(a => a.PlanId == plan.PlanId);
            if (existingPlan != null)
            {
                _repository.Update(plan);
                return true;
            }
            throw new PlanNotFoundException("No such plan found");
        }
    }
}
