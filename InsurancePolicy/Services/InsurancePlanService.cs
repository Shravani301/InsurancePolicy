using AutoMapper;
using InsurancePolicy.DTOs;
using InsurancePolicy.Exceptions.PlanExceptions;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class InsurancePlanService:IInsurancePlanService
    {
        private readonly IRepository<InsurancePlan> _repository;
        private readonly IMapper _mapper;

        public InsurancePlanService(IRepository<InsurancePlan> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Guid Add(InsurancePlanRequestDto planRequest)
        {
            var plan = _mapper.Map<InsurancePlan>(planRequest);
            _repository.Add(plan);
            return plan.PlanId;
        }

        public void Delete(Guid id)
        {
            var plan = _repository.GetById(id);
            if (plan == null)
                throw new PlanNotFoundException("No such plan found to delete");

            _repository.Delete(plan);
        }

        public InsurancePlanResponseDto GetById(Guid id)
        {
            var plan = _repository.GetAll()
                .Include(p => p.Schemes)
                .FirstOrDefault(p => p.PlanId == id);

            if (plan == null)
                throw new PlanNotFoundException("No such plan found");

            return _mapper.Map<InsurancePlanResponseDto>(plan);
        }

        public List<InsurancePlanResponseDto> GetAll()
        {
            var plans = _repository.GetAll()
                .Include(p => p.Schemes)
                .ToList();

            if (!plans.Any())
                throw new PlansDoesNotExistException("No plans exist");

            return _mapper.Map<List<InsurancePlanResponseDto>>(plans);
        }

        public void Update(InsurancePlanRequestDto planRequest)
        {
            var existingPlan = _repository.GetAll()
         .Include(p => p.Schemes) // Include related schemes
         .FirstOrDefault(p => p.PlanId == planRequest.PlanId);

            if (existingPlan == null)
                throw new PlanNotFoundException("No such plan found");

            // Update the plan details
            _mapper.Map(planRequest, existingPlan);

            // If the plan status is set to false, deactivate related schemes
            if (!existingPlan.Status && existingPlan.Schemes != null)
            {
                foreach (var scheme in existingPlan.Schemes)
                {
                    scheme.Status = false;
                }
            }

            _repository.Update(existingPlan);

        }
    }
}
