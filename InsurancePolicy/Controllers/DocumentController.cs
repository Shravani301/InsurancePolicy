using InsurancePolicy.Models;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _service;
        public DocumentController(IDocumentService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var documents = _service.GetAll();
            return Ok(documents);
        }

        //[HttpGet("{id}")]
        //public IActionResult Get(string id)
        //{
        //    var document = _service.GetById(id);
        //    return Ok(document);
        //}

        [HttpPost]
        public IActionResult Add(Document document)
        {
            var newDocument = _service.Add(document);
            return Ok(newDocument);
        }

        [HttpPut]
        public IActionResult Modify(Document document)
        {
            _service.Update(document);
            return Ok(document);
        }

        //[HttpDelete("{id}")]
        //public IActionResult Delete(string id)
        //{
        //    _service.Delete(id);
        //    return Ok("Deleted Successfully!");
        //}
    }
}
