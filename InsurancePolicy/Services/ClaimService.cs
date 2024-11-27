using InsurancePolicy.Exceptions.ClaimExceptions;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class ClaimService:IClaimService
    {
        private readonly IRepository<Claim> _repository;
        public ClaimService(IRepository<Claim> repository)
        {
            _repository = repository;
        }
        public Guid Add(Claim claim)
        {
            _repository.Add(claim);
            return claim.ClaimId;
        }

        public bool Delete(Guid id)
        {
            var claim = _repository.GetById(id);
            if (claim != null)
            {
                _repository.Delete(claim);
                return true;
            }
            throw new ClaimNotFoundException("No such claim found to delete");
        }

        public Claim GetById(Guid id)
        {
            var claim = _repository.GetById(id);
            if (claim != null)
                return claim;
            throw new ClaimNotFoundException("No such claim found");
        }

        public List<Claim> GetAll()
        {
            var claims = _repository.GetAll().ToList();
            if (claims.Count == 0)
                throw new ClaimsDoesNotExistException("No claims Exist");
            return claims;
        }

        public bool Update(Claim claim)
        {
            var existingAdmin = _repository.GetAll().AsNoTracking().FirstOrDefault(a => a.ClaimId == claim.ClaimId);
            if (existingAdmin != null)
            {
                _repository.Update(claim);
                return true;
            }
            throw new ClaimNotFoundException("No such claim found");
        }
    }
}
