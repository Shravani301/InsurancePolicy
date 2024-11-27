using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface IClaimService
    {
        public List<Claim> GetAll();
        public Claim GetById(Guid id);
        public Guid Add(Claim claim);
        public bool Update(Claim claim);
        public bool Delete(Guid id);
    }
}
