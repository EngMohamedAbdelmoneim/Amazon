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
                return NotFound();
            }
            return Ok(cartDto);
        }

        [HttpPost]
        public async Task<ActionResult<CartDto>> SetCart(CartDto cartDto)
        {
            var newCartDto = await _cartService.SetCartAsync(cartDto);
            if (newCartDto == null)
            {
                return BadRequest("Failed to set cart.");
            }

            return Ok(newCartDto);
        }

        [HttpDelete("{cartId}")]
        public async Task<IActionResult> DeleteCart(string cartId)
        {
            var result = await _cartService.RemoveCartAsync(cartId);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
