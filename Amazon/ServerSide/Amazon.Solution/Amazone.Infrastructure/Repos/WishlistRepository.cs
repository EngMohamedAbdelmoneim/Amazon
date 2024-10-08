using Amazon.Core.Entities;
using Amazone.Infrastructure.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Amazone.Infrastructure.Repos
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly IDatabase _database;
        public WishlistRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<Wishlist?> GetWishlistAsync(string wishlistId)
        {
            var data = await _database.StringGetAsync(wishlistId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Wishlist>(data);
        }

        public async Task<Wishlist?> CreateOrUpdateWishlistAsync(Wishlist wishlist)
        {
            var created = await _database.StringSetAsync(wishlist.Id,
                JsonSerializer.Serialize(wishlist), TimeSpan.FromDays(30));

            return !created ? null : await GetWishlistAsync(wishlist.Id);
        }

        public async Task<bool> DeleteWishlistAsync(string wishlistId)
        {
            return await _database.KeyDeleteAsync(wishlistId);
        }
    }
}
