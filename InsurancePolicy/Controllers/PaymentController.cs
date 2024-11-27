using InsurancePolicy.Models;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InsurancePolicy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;
        public PaymentController(IPaymentService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var payments = _service.GetAll();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var payment = _service.GetById(id);
            return Ok(payment);
        }

        [HttpPost]
        public IActionResult Add(Payment payment)
        {
            var newPayment = _service.Add(payment);
            return Ok(newPayment);
        }
    }
}
