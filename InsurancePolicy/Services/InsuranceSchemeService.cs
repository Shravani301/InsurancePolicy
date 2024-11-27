using InsurancePolicy.Exceptions.SchemeExceptions;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class InsuranceSchemeService:IInsuranceSchemeService
    {
        private readonly IRepository<InsuranceScheme> _repository;
        public InsuranceSchemeService(IRepository<InsuranceScheme> repository)
        {
            _repository = repository;
        }
        public Guid Add(InsuranceScheme scheme)
        {
            _repository.Add(scheme);
            return scheme.SchemeId;
        }

        public bool Delete(Guid id)
        {
            var scheme = _repository.GetById(id);
            if (scheme != null)
            {
                _repository.Delete(scheme);
                return true;
            }
            throw new SchemeNotFoundException("No such scheme found to delete");
        }

        public InsuranceScheme GetById(Guid id)
        {
            var scheme = _repository.GetById(id);
            if (scheme != null)
                return scheme;
            throw new SchemeNotFoundException("No such scheme found");
        }

        public List<InsuranceScheme> GetAll()
        {
            var schemes = _repository.GetAll().ToList();
            if (schemes.Count == 0)
                throw new SchemesDoesNotExistException("schemes does not exist!");
            return schemes;
        }

        public bool Update(InsuranceScheme scheme)
        {
            var existingScheme = _repository.GetAll().AsNoTracking().FirstOrDefault(a => a.SchemeId == scheme.SchemeId);
            if (existingScheme != null)
            {
                _repository.Update(scheme);
                return true;
            }
            throw new SchemeNotFoundException("No such schemes found");
        }
    }
}
