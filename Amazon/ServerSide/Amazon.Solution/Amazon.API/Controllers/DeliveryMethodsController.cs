using Amazon.Services.DeliveryMethodService;
using Amazon.Services.DeliveryMethodService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class DeliveryMethodsController : ControllerBase
	{
		private readonly IDeliveryMethodService _deliveryMethodService;

		public DeliveryMethodsController(IDeliveryMethodService deliveryMethodService)
        {
			_deliveryMethodService = deliveryMethodService;
		}



        [HttpGet]
		public async Task<ActionResult<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethods()
			=> Ok(await _deliveryMethodService.GetAllDeliveryMethodsAsync());
		
		[HttpGet("{id}")]
		public async Task<ActionResult<DeliveryMethodDto>> GetDeliveryMethodById(int id)
			=> Ok(await _deliveryMethodService.GetDeliveryMethodByIdAsync(id));
	}
}
