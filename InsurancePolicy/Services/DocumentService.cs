using InsurancePolicy.Exceptions.DocumentExceptions;
using InsurancePolicy.Models;
using InsurancePolicy.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsurancePolicy.Services
{
    public class DocumentService:IDocumentService
    {
        private readonly IRepository<Document> _repository;
        public DocumentService(IRepository<Document> repository)
        {
            _repository = repository;
        }
        public string Add(Document document)
        {
            _repository.Add(document);
            return document.DocumentId;
        }

        //public bool Delete(string id)
        //{
        //    var document = _repository.GetById(id);
        //    if (document != null)
        //    {
        //        _repository.Delete(document);
        //        return true;
        //    }
        //    throw new DocumentNotFoundException("No such document found to delete");
        //}

        //public Document GetById(string id)
        //{
        //    var document = _repository.GetById(id);
        //    if (document != null)
        //        return document;
        //    throw new DocumentNotFoundException("No such document found");
        //}

        public List<Document> GetAll()
        {
            var documents = _repository.GetAll().Include(c=>c.Customer).ToList();
            if (documents.Count == 0)
                throw new DocumentsDoesNotExistException("Documents does not exist!");
            return documents;
        }

        public bool Update(Document document)
        {
            var existingCustomer = _repository.GetAll().AsNoTracking().FirstOrDefault(a => a.DocumentId == document.DocumentId);
            if (existingCustomer != null)
            {
                _repository.Update(document);
                return true;
            }
            throw new DocumentNotFoundException("No such document found");
        }
    }
}
