using Amazon.Services.WishlistService.Dto;
using Amazon.Services.WishlistService;
using Microsoft.AspNetCore.Mvc;
using Amazon.Services.WishlistService;

namespace Amazon.API.Controllers
{
    public class WishlistController : BaseController
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService WishlistService)
        {
            _wishlistService = WishlistService;
        }

        [HttpGet("{wishlistId}")]
        public async Task<ActionResult<WishlistDto>> GetWishlistById(string wishlistId)
        {
            var wishlistDto = await _wishlistService.GetWishlistByIdAsync(wishlistId);
            if (wishlistDto == null)
            {
                return NotFound();
            }
            return Ok(wishlistDto);
        }

        [HttpPost]
        public async Task<ActionResult<WishlistDto>> SetWishlist(WishlistDto wishlistDto)
        {
            var newWishlistDto = await _wishlistService.SetWishlistAsync(wishlistDto);
            if (newWishlistDto == null)
            {
                return BadRequest("Failed to set wishlist.");
            }

            return Ok(newWishlistDto);
        }

        [HttpDelete("{wishlistId}")]
        public async Task<IActionResult> DeleteCart(string wishlistId)
        {
            var result = await _wishlistService.RemoveWishlistAsync(wishlistId);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
