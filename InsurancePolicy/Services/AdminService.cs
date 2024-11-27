using InsurancePolicy.Exceptions.AdminExceptions;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class AdminService:IAdminService
    {
        private readonly IRepository<Admin> _repository;
        public AdminService(IRepository<Admin> repository)
        {
            _repository = repository;
        }
        public Guid Add(Admin admin)
        {
            _repository.Add(admin);
            return admin.PaymentId;
        }

        public bool Delete(Guid id)
        {
            var admin = _repository.GetById(id);
            if (admin != null)
            {
                _repository.Delete(admin);
                return true;
            }
            throw new PlanNotFoundException("No such admin found to delete");
        }

        public Admin GetById(Guid id)
        {
            var admin = _repository.GetById(id);
            if (admin != null)
                return admin;
            throw new PlanNotFoundException("No such admin found");
        }

        public List<Admin> GetAll()
        {
            var admins = _repository.GetAll().ToList();
            if (admins.Count == 0)
                throw new AdminsDoesNotExistException("No admins Exist");
            return admins;
        }

        public bool Update(Admin admin)
        {
            var existingAdmin = _repository.GetAll().AsNoTracking().FirstOrDefault(a => a.PaymentId == admin.PaymentId);
            if (existingAdmin != null)
            {
                _repository.Update(admin);
                return true;
            }
            throw new PlanNotFoundException("No such role found");
        }
    }
}
