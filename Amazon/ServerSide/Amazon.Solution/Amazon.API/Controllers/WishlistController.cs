using Amazon.Services.WishlistService.Dto;
using Amazon.Services.WishlistService;
using Microsoft.AspNetCore.Mvc;
using Amazon.API.Errors;

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
                return null;
            }
            return Ok(wishlistDto);
        }

        [HttpPost]
        public async Task<ActionResult<WishlistDto>> SetWishlist(string wishlistId, WishlistDto wishlistDto)
        {
            if (string.IsNullOrEmpty(wishlistId) || wishlistId != wishlistDto.Id)
            {
                return BadRequest(new ApiResponse(400,"Invalid wishlist ID or wishlist data."));
            }

			foreach (var item in wishlistDto.Items.ToList())
			{
				if (item.Quantity == 0)
				{
					wishlistDto.Items.Remove(item);
				}
			}

			if (wishlistDto.Items.Count() == 0)
			{
				await _wishlistService.RemoveWishlistAsync(wishlistId);
				return Ok("WishList is Empty");

			}

			var newWishlistDto = await _wishlistService.SetWishlistAsync(wishlistId ,wishlistDto);
            if (newWishlistDto == null)
            {
                return BadRequest(new ApiResponse(400,"Failed to set wishlist."));
            }

            return Ok(newWishlistDto);
        }

        [HttpDelete("{wishlistId}")]
        public async Task<IActionResult> DeleteWishlist(string wishlistId)
        {
            var result = await _wishlistService.RemoveWishlistAsync(wishlistId);
            if (!result)
            {
                return NotFound(new ApiResponse(404));
            }

            return NoContent();
        }
    }
}
