using InsurancePolicy.DTOs;
using InsurancePolicy.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InsurancePolicy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public IActionResult AddPayment(PaymentRequestDto paymentDto)
        {
            var paymentId = _paymentService.AddPayment(paymentDto);
            return Ok(new { PaymentId = paymentId, Message = "Payment added successfully." });
        }

        [HttpGet("policy/{policyId}")]
        public IActionResult GetPaymentsByPolicy(Guid policyId)
        {
            var payments = _paymentService.GetPaymentsByPolicy(policyId);
            return Ok(payments);
        }

        [HttpGet]
        public IActionResult GetAllPayments()
        {
            var payments = _paymentService.GetAllPayments();
            return Ok(payments);
        }
    }
}
