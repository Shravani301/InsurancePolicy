using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace InsurancePolicy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StripeController : ControllerBase
    {
        public StripeController()
        {
            StripeConfiguration.ApiKey = "sk_test_51OYCJzSJHtcsvReHv2qfgF0naXXXAN8BTkSexJuDZDcsGImYN1uLiqMb0r2F3vXU7OTb08QlZJUOhtTtkGu72x3k00y0RcoaXY"; // Your secret key
        }

        /// <summary>
        /// Create a new Stripe Checkout Session
        /// </summary>
        /// <param name="request">Checkout Request containing the amount</param>
        /// <returns>Session ID</returns>
        [HttpPost("create-checkout-session")]
        public IActionResult CreateCheckoutSession([FromBody] CheckoutRequest request)
        {
            try
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                Currency = "usd",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = "Your Product",
                                },
                                UnitAmount = request.Amount, // Amount in cents (e.g., $10 = 1000 cents)
                            },
                            Quantity = 1,
                        },
                    },
                    Mode = "payment",
                    SuccessUrl = "http://localhost:4200/success?session_id={CHECKOUT_SESSION_ID}", // Replace with your success page
                    CancelUrl = "http://localhost:4200/cancel", // Replace with your cancel page
                };

                var service = new SessionService();
                var session = service.Create(options);

                return Ok(new { sessionId = session.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Retrieve the payment status using Checkout Session ID
        /// </summary>
        /// <param name="sessionId">The session ID to retrieve details</param>
        /// <returns>Payment status and details</returns>
        [HttpGet("payment-status/{sessionId}")]
        public IActionResult GetPaymentStatus(string sessionId)
        {
            try
            {
                var sessionService = new SessionService();
                var session = sessionService.Get(sessionId);

                // Retrieve the Payment Intent for additional details
                var paymentIntentService = new PaymentIntentService();
                var paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

                return Ok(new
                {
                    SessionId = session.Id,
                    PaymentIntentId = paymentIntent.Id,
                    PaymentStatus = session.PaymentStatus, // e.g., "paid", "unpaid"
                    Status = paymentIntent.Status, // e.g., "succeeded", "requires_payment_method"
                    Amount = paymentIntent.Amount,
                    Currency = paymentIntent.Currency,
                    CustomerEmail = session.CustomerDetails?.Email
                });
            }
            catch (StripeException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }

    /// <summary>
    /// Model for Checkout Request
    /// </summary>
    public class CheckoutRequest
    {
        public long Amount { get; set; } // Amount in cents
    }
}
