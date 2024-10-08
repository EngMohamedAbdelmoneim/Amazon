using Amazon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Infrastructure.Interfaces
{
    public interface IWishlistRepository
    {
        Task<Wishlist?> GetWishlistAsync(string wishlistId);
        Task<Wishlist?> CreateOrUpdateWishlistAsync(Wishlist wishlist);
        Task<bool> DeleteWishlistAsync(string wishlistId);
    }
}
