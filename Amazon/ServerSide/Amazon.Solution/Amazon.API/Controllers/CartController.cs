using Amazon.API.Errors;
using Amazon.Core.Entities;
using Amazon.Services.BrandService;
using Amazon.Services.CartService;
using Amazon.Services.CartService.Dto;
using Amazon.Services.CategoryServices;
using Amazon.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.API.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("{cartId}")]
        public async Task<ActionResult<CartDto>> GetCartById(string cartId)
        {
            var cartDto = await _cartService.GetCartByIdAsync(cartId);
            if (cartDto == null)
            {
                return null;
            }
            return Ok(cartDto);
        }

        [HttpPost]
        public async Task<ActionResult<CartDto>> SetCart(string cartId, CartDto cartDto)
        {
            if (string.IsNullOrEmpty(cartId) || cartId != cartDto.Id)
            {
                return BadRequest("Invalid cart ID or cart data.");
            }

			foreach (var item in cartDto.Items.ToList())
			{
				if (item.Quantity == 0)
				{
					cartDto.Items.Remove(item);
				}
			}

			if (cartDto.Items.Count() == 0)
			{
				await _cartService.RemoveCartAsync(cartId);
				return Ok("Cart is Empty");

			}

			var newCartDto = await _cartService.SetCartAsync(cartId, cartDto);
            if (newCartDto == null)
            {
                return BadRequest(new ApiResponse(400, "Failed to set cart."));
            }

            return Ok(newCartDto);
        }

        [HttpDelete("{cartId}")]
        public async Task<IActionResult> DeleteCart(string cartId)
        {
            var result = await _cartService.RemoveCartAsync(cartId);
            if (!result)
            {
                return NotFound(new ApiResponse(404));
            }

            return NoContent();
        }
    }
}
