using Amazon.Core.Entities.OrderAggregate;
using Amazon.Services.CartService.Dto;
using Amazon.Services.PaymentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Amazon.API.Controllers
{
    public class PaymentsController : BaseController
    {
        private const string WhSecret = "";
        private readonly IPaymentService _paymentService;
        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Authorize]
        [HttpPost("{cartId}")]
        public async Task<ActionResult<CartDto>> SetPaymentIntent(string cartId)
        {
            var cartDto = await _paymentService.SetPaymentIntent(cartId);
            if (cartDto == null)
            {
                return BadRequest("Problem with cart.");
            }

            return Ok(cartDto);
        }

        [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebHook()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json, 
                Request.Headers["Stripe-Signature"], WhSecret);

            PaymentIntent intent;
            Order order;
            switch(stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                    break;
                case "payment_intent.failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                    break;
            }

            return new EmptyResult();
        }
    }
}
