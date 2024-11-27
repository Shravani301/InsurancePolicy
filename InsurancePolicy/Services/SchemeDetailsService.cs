using InsurancePolicy.Exceptions.RoleException;
using InsurancePolicy.Exceptions.SchemeDetailsExceptions;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class SchemeDetailsService:ISchemeDetailsService
    {
        private readonly IRepository<SchemeDetails> _repository;
        public SchemeDetailsService(IRepository<SchemeDetails> repository)
        {
            _repository = repository;
        }
        public Guid Add(SchemeDetails schemeDetails)
        {
            _repository.Add(schemeDetails);
            return schemeDetails.DetailId;
        }

        public bool Delete(Guid id)
        {
            var schemeDetails = _repository.GetById(id);
            if (schemeDetails == null)
            {
                throw new RoleNotFoundException("No such role found to delete");
            }
            _repository.Delete(schemeDetails);
            return true;

        }

        public SchemeDetails GetById(Guid id)
        {
            var schemeDetails = _repository.GetById(id);
            if (schemeDetails != null)
                return schemeDetails;
            throw new SchemeDetailsNotFoundException("No such role found");
        }

        public List<SchemeDetails> GetAll()
        {
            var schemeDetails = _repository.GetAll().ToList();
            if (schemeDetails.Count != 0)
                return schemeDetails;
            throw new SchemeDetailsDoesNotExistException("No roles Exist");
        }

        public bool Update(SchemeDetails schemeDetails)
        {
            var existingSchemeDetails = _repository.GetAll().AsNoTracking().FirstOrDefault(a => a.DetailId == schemeDetails.DetailId);
            if (existingSchemeDetails != null)
            {
                _repository.Update(schemeDetails);
                return true;
            }
            throw new SchemeDetailsNotFoundException("No such role found");
        }
    }
}
