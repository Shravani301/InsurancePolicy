using AutoMapper;
using InsurancePolicy.DTOs;
using InsurancePolicy.Helpers;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class InstallmentService : IInstallmentService
    {
        private readonly IRepository<Installment> _installmentRepository;
        private readonly IMapper _mapper;

        public InstallmentService(IRepository<Installment> installmentRepository, IMapper mapper)
        {
            _installmentRepository = installmentRepository;
            _mapper = mapper;
        }

        public Guid AddInstallment(InstallmentRequestDto installmentDto)
        {
            var installment = _mapper.Map<Installment>(installmentDto);
            _installmentRepository.Add(installment);
            return installment.InstallmentId;
        }

        public bool UpdateInstallment(InstallmentRequestDto installmentDto)
        {
            var existingInstallment = _installmentRepository.GetById(installmentDto.InstallmentId.Value);
            if (existingInstallment == null)
                throw new KeyNotFoundException("Installment not found.");

            _mapper.Map(installmentDto, existingInstallment);
            _installmentRepository.Update(existingInstallment);
            return true;
        }

        public InstallmentResponseDto GetInstallmentById(Guid installmentId)
        {
            var installment = _installmentRepository.GetAll()
                .Include(i => i.InsurancePolicy)
                .ThenInclude(p => p.InsuranceScheme)
                .FirstOrDefault(i => i.InstallmentId == installmentId);

            if (installment == null)
                throw new KeyNotFoundException("Installment not found.");

            return _mapper.Map<InstallmentResponseDto>(installment);
        }

        public List<InstallmentResponseDto> GetAllInstallmentsForPolicy(Guid policyId)
        {
            var installments = _installmentRepository.GetAll()
                .Where(i => i.PolicyId == policyId)
                .ToList();

            return _mapper.Map<List<InstallmentResponseDto>>(installments);
        }
        public PageList<InstallmentResponseDto> GetPaginatedInstallmentsForPolicy(Guid policyId, PageParameters pageParameters)
        {
            var installments = _installmentRepository.GetAll()
                .Where(i => i.PolicyId == policyId)
                .ToList();

            var paginatedInstallments = PageList<InstallmentResponseDto>.ToPagedList(
                _mapper.Map<List<InstallmentResponseDto>>(installments),
                pageParameters.PageNumber,
                pageParameters.PageSize
            );

            return paginatedInstallments;
        }
        public bool DeleteInstallment(Guid installmentId)
        {
            var installment = _installmentRepository.GetById(installmentId);
            if (installment == null)
                throw new KeyNotFoundException("Installment not found.");

            _installmentRepository.Delete(installment);
            return true;
        }
    }
}
