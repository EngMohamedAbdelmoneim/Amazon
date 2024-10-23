using Amazon.API.Errors;
using Amazon.Services.PaymentMethodService;
using Amazon.Services.PaymentMethodService.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentMethodsController : ControllerBase
	{
		private readonly IPaymentMethodService _paymentMethodService;

		public PaymentMethodsController(IPaymentMethodService paymentMethodService)
		{
			_paymentMethodService = paymentMethodService;
		}



		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<PaymentMethodDto>>> GetPaymentMethods()
			=> Ok(await _paymentMethodService.GetAllPaymentMethodsAsync());

		[HttpGet("{id}")]
		public async Task<ActionResult<PaymentMethodDto>> GetPaymentMethodById(int id)
		{
			var result = await _paymentMethodService.GetPaymentMethodByIdAsync(id);
			if (result is null)
				return NotFound(new ApiResponse(404));
			return Ok();

		}
	}
}
