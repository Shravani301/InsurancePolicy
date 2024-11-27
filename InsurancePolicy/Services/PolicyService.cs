
using InsurancePolicy.Exceptions.PolicyExceptions;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class PolicyService:IPolicyService
    {
        private readonly IRepository<Policy> _repository;
        public PolicyService(IRepository<Policy> repository)
        {
            _repository = repository;
        }
        public Guid Add(Policy policy)
        {
            _repository.Add(policy);
            return policy.PolicyId;
        }

        public bool Delete(Guid id)
        {
            var policy = _repository.GetById(id);
            if (policy != null)
            {
                _repository.Delete(policy);
                return true;
            }
            throw new PolicyNotFoundException("No such policy found to delete");
        }

        public Policy GetById(Guid id)
        {
            var policy = _repository.GetById(id);
            if (policy != null)
                return policy;
            throw new PolicyNotFoundException("No such policy found");
        }

        public List<Policy> GetAll()
        {
            var ploicies = _repository.GetAll().ToList();
            if (ploicies.Count == 0)
                throw new PoliciesDoesNotExistException("No policis Exist");
            return ploicies;
        }

        public bool Update(Policy policy)
        {
            var existingPolicy = _repository.GetAll().AsNoTracking().FirstOrDefault(a => a.PolicyId == policy.PolicyId);
            if (existingPolicy != null)
            {
                _repository.Update(policy);
                return true;
            }
            throw new PolicyNotFoundException("No such policy found");
        }
    }
}
