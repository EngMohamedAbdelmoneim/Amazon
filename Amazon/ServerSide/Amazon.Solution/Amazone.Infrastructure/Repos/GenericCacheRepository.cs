using Amazone.Infrastructure.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Amazone.Infrastructure.Repos
{
    public class GenericCacheRepository<T> : IGenericCacheRepository<T> where T : class
    {
        private readonly IDatabase _database;

        public GenericCacheRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<T?> GetAsync(string id)
        {
            var data = await _database.StringGetAsync(id);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<T>(data);
        }

        public async Task<T?> CreateOrUpdateAsync(string id, T entity)
        {
            var created = await _database.StringSetAsync(id, 
                JsonSerializer.Serialize(entity), TimeSpan.FromDays(30));
            return !created ? null : await GetAsync(id);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }
    }
}
