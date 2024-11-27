using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface IAdminService
    {
        public List<Admin> GetAll();
        public Admin GetById(Guid id);
        public Guid Add(Admin admin);
        public bool Update(Admin admin);
        public bool Delete(Guid id);
    }
}
