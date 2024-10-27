using Amazon.Services.OrderService.OrderDto;
using Amazon.Services.OrderService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Amazon.Core.Entities.OrderAggregate;
using Microsoft.AspNetCore.Identity;
using Amazon.Core.Entities.Identity;
using Amazon.API.Extentions;
using Amazon.API.Errors;

namespace Amazon.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderService _orderService;
		private readonly UserManager<AppUser> _userManager;

		public OrderController(IOrderService orderService, UserManager<AppUser> userManager)
		{

			_orderService = orderService;
			_userManager = userManager;
		}

		[Authorize]
		[HttpPost]
		public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
		{
			var buyerEmail = User.FindFirstValue("Email");
			var order = await _orderService.CreateOrderAsync(buyerEmail, orderDto.CartId,orderDto.PaymentMethodId,orderDto.DeliveryMethodId,orderDto.ShippingAddressId);
			if (order == null)
				return BadRequest(new ApiResponse(400));

			return Ok(order);
		}

		[Authorize]
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetAllOrderesForUser()
		{

			var user = await _userManager.FindUserWithAddressAsync(User);
			var buyerEmail = user.Email;

			var orders = await _orderService.GetOrdersForUserAsync(buyerEmail);
			return Ok(orders);
		}

		[Authorize]
		[HttpGet("{id}")]
		public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id)
		{
			var user = await _userManager.FindUserWithAddressAsync(User);
			var buyerEmail = user.Email;

			var order = await _orderService.GetOrderByIdForUserAsync(id,buyerEmail);
			if (order == null) 
				return NotFound(new ApiResponse(404));
			return Ok(order);
		}

		[Authorize]
		[HttpPost("cancel/{orderId}")]
		public async Task<IActionResult> CancelOrder(int orderId)
		{
			var buyerEmail = User.FindFirstValue("Email");

			var result = await _orderService.CancelOrderAsync(orderId, buyerEmail);
			if (result is null)
				return BadRequest(new ApiResponse(400, "Order cancellation failed or is not allowed."));

			return Ok(new ApiResponse(200, "Order has been canceled successfully."));
		}
	}
}
