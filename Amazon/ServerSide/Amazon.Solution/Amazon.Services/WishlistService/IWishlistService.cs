using Amazon.Services.CartService.Dto;
using Amazon.Services.WishlistService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.WishlistService
{
    public interface IWishlistService
    {
        Task<WishlistDto> GetWishlistByIdAsync(string wishlistId);
        Task<WishlistDto> SetWishlistAsync(string wishlistId, WishlistDto wishlistDto);
        Task<bool> RemoveWishlistAsync(string wishlistId);
    }
}
