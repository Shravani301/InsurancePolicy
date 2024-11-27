using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface ISchemeDetailsService
    {
        public List<SchemeDetails> GetAll();
        public SchemeDetails GetById(Guid id);
        public Guid Add(SchemeDetails schemeDetails);
        public bool Update(SchemeDetails scheme);
        public bool Delete(Guid id);
    }
}
