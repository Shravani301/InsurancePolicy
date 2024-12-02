using InsurancePolicy.Models;

namespace InsurancePolicy.Services
{
    public interface IDocumentService
    {
        public List<Document> GetAll();
        //public Document GetById(string id);
        public string Add(Document document);
        public bool Update(Document document);
        //public bool Delete(string id);
    }
}
