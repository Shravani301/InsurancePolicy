using AutoMapper;
using InsurancePolicy.DTOs;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;

namespace InsurancePolicy.Services
{
    public class CommissionService : ICommissionService
    {
        private readonly IRepository<Commission> _commissionRepository;
        private readonly IRepository<Policy> _policyRepository;
        private readonly IRepository<Agent> _agentRepository;
        private readonly IMapper _mapper;

        public CommissionService(
            IRepository<Commission> commissionRepository,
            IRepository<Policy> policyRepository,
            IRepository<Agent> agentRepository,
            IMapper mapper)
        {
            _commissionRepository = commissionRepository;
            _policyRepository = policyRepository;
            _agentRepository = agentRepository;
            _mapper = mapper;
        }

        public Guid AddCommission(CommissionRequestDto commissionDto)
        {
            // Validate agent existence
            var agent = _agentRepository.GetById(commissionDto.AgentId);
            if (agent == null)
                throw new ArgumentException("Invalid Agent ID.");

            // Validate policy linkage if provided
            if (commissionDto.PolicyNo.HasValue)
            {
                var policy = _policyRepository.GetById(commissionDto.PolicyNo.Value);
                if (policy == null)
                    throw new ArgumentException("Invalid Policy ID.");
            }

            // Map and save commission
            var commission = _mapper.Map<Commission>(commissionDto);
            _commissionRepository.Add(commission);

            return commission.CommissionId;
        }

        public List<CommissionResponseDto> GetCommissionsByAgent(Guid agentId)
        {
            var commissions = _commissionRepository.GetAll()
                .Where(c => c.AgentId == agentId)
                .ToList();

            return _mapper.Map<List<CommissionResponseDto>>(commissions);
        }

        public List<CommissionResponseDto> GetCommissionsByPolicy(Guid policyId)
        {
            var commissions = _commissionRepository.GetAll()
                .Where(c => c.PolicyNo == policyId)
                .ToList();

            return _mapper.Map<List<CommissionResponseDto>>(commissions);
        }

        public List<CommissionResponseDto> GetAllCommissions()
        {
            var commissions = _commissionRepository.GetAll().ToList();
            return _mapper.Map<List<CommissionResponseDto>>(commissions);
        }
    }
}
