using Amazon.Core.Entities.OrderAggregate;
using Amazon.Services.CartService.Dto;
using Amazon.Services.PaymentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Amazon.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentsController : ControllerBase
	{
		private const string _whSecret = "";
		private readonly IPaymentService _paymentService;
        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [Authorize]
		[HttpPost("{cartId}")] //  api/payments/cartId
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
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
			const string endpointSecret = _whSecret;

			var stripeEvent = EventUtility.ParseEvent(json);
			var signatureHeader = Request.Headers["Stripe-Signature"];

			stripeEvent = EventUtility.ConstructEvent(json,
					signatureHeader, endpointSecret);

			var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

			Order order;
            if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
                order = await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentIntent.Id, true);
            else
                order = await _paymentService.UpdatePaymentIntentToSucceededOrFailed(paymentIntent.Id, false);
            

			return Ok();
		}
    }
}
