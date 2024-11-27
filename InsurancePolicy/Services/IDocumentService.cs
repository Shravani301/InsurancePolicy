using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface IDocumentService
    {
        public List<Document> GetAll();
        public Document GetById(Guid id);
        public Guid Add(Document document);
        public bool Update(Document document);
        public bool Delete(Guid id);
    }
}
