using AutoMapper;
using InsurancePolicy.DTOs;
using InsurancePolicy.Exceptions.PlanExceptions;
using InsurancePolicy.Helpers;
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
        public void Activate(Guid id)
        {
            var plan = _repository.GetById(id);
            if (plan == null)
                throw new PlanNotFoundException("No such plan found to delete");

            _repository.Activate(plan);
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
        public PageList<InsurancePlanResponseDto> GetAllPaginated(PageParameters pageParameters)
        {
            // Fetch only active plans
            var activePlans = _repository.GetAll().ToList();

            // Map to response DTO and apply pagination
            var paginatedPlans = PageList<InsurancePlanResponseDto>.ToPagedList(
                _mapper.Map<List<InsurancePlanResponseDto>>(activePlans),
                pageParameters.PageNumber,
                pageParameters.PageSize
            );

            return paginatedPlans;
        }


        public List<InsurancePlanResponseDto> GetAll()
        {
            // Fetch only active plans
            var activePlans = _repository.GetAll()
                .Include(p => p.Schemes)
                .Where(p => p.Status) // Filter plans with Status == true
                .ToList();

            if (!activePlans.Any())
                throw new PlansDoesNotExistException("No active plans exist");

            return _mapper.Map<List<InsurancePlanResponseDto>>(activePlans);
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
