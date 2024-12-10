using InsurancePolicy.DTOs;
using InsurancePolicy.Helpers;
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
        public IActionResult GetAllPayments([FromQuery] PageParameters pageParameters)
        {
            var payments = _paymentService.GetAllPaginated(pageParameters);

            // Add pagination metadata to headers
            Response.Headers.Add("X-Total-Count", payments.TotalCount.ToString());
            Response.Headers.Add("X-Page-Size", payments.PageSize.ToString());
            Response.Headers.Add("X-Current-Page", payments.CurrentPage.ToString());
            Response.Headers.Add("X-Total-Pages", payments.TotalPages.ToString());
            Response.Headers.Add("X-Has-Next", payments.HasNext.ToString());
            Response.Headers.Add("X-Has-Previous", payments.HasPrevious.ToString());

            // Return the paginated payments in the response body
            return Ok(payments);
        }

    }
}
