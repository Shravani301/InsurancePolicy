using AutoMapper;
using InsurancePolicy.DTOs;
using InsurancePolicy.Exceptions.SchemeExceptions;
using InsurancePolicy.Exceptions.PlanExceptions; // Import PlanNotFoundException
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class InsuranceSchemeService : IInsuranceSchemeService
    {
        private readonly IRepository<InsuranceScheme> _repository;
        private readonly IRepository<InsurancePlan> _planRepository;
        private readonly IMapper _mapper;

        public InsuranceSchemeService(IRepository<InsuranceScheme> repository, IRepository<InsurancePlan> planRepository, IMapper mapper)
        {
            _repository = repository;
            _planRepository = planRepository;
            _mapper = mapper;
        }

        public List<InsuranceSchemeResponseDto> GetAll()
        {
            var schemes = _repository.GetAll()
                .Include(s => s.InsurancePlan)
                .Include(s => s.Policies)
                .ToList();

            if (!schemes.Any())
                throw new SchemesDoesNotExistException("No schemes found.");

            return _mapper.Map<List<InsuranceSchemeResponseDto>>(schemes);
        }

        public InsuranceSchemeResponseDto GetById(Guid id)
        {
            var scheme = _repository.GetAll()
                .Include(s => s.InsurancePlan)
                .Include(s => s.Policies)
                .FirstOrDefault(s => s.SchemeId == id);

            if (scheme == null)
                throw new SchemeNotFoundException("No such scheme found.");

            return _mapper.Map<InsuranceSchemeResponseDto>(scheme);
        }

        public Guid Add(InsuranceSchemeRequestDto schemeDto)
        {
            // Check if the associated plan exists and is active
            var plan = _planRepository.GetById(schemeDto.PlanId);
            if (plan == null || !plan.Status)
            {
                throw new PlanNotFoundException("Plan is deactivated.");
            }

            var scheme = _mapper.Map<InsuranceScheme>(schemeDto);

            if (scheme.MinAmount > scheme.MaxAmount)
                throw new ArgumentException("Minimum amount cannot be greater than the maximum amount.");

            if (scheme.MinInvestTime > scheme.MaxInvestTime)
                throw new ArgumentException("Minimum investment time cannot be greater than the maximum investment time.");

            _repository.Add(scheme);
            return scheme.SchemeId;
        }

        public bool Update(InsuranceSchemeRequestDto schemeDto)
        {
            // Check if the associated plan exists and is active
            var plan = _planRepository.GetById(schemeDto.PlanId);
            if (plan == null || !plan.Status)
            {
                throw new PlanNotFoundException("Plan is deactivated.");
            }

            var existingScheme = _repository.GetAll()
                .FirstOrDefault(s => s.SchemeId == schemeDto.SchemeId);

            if (existingScheme == null)
                throw new SchemeNotFoundException("No such scheme found.");

            var updatedScheme = _mapper.Map(schemeDto, existingScheme);

            if (updatedScheme.MinAmount > updatedScheme.MaxAmount)
                throw new ArgumentException("Minimum amount cannot be greater than the maximum amount.");

            if (updatedScheme.MinInvestTime > updatedScheme.MaxInvestTime)
                throw new ArgumentException("Minimum investment time cannot be greater than the maximum investment time.");

            _repository.Update(updatedScheme);
            return true;
        }

        public bool Delete(Guid id)
        {
            var scheme = _repository.GetById(id);

            if (scheme == null)
                throw new SchemeNotFoundException("No such scheme found.");

            _repository.Delete(scheme);
            return true;
        }
    }
}
