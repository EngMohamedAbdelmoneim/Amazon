using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Infrastructure.Interfaces
{
    public interface IGenericCacheRepository<T> where T : class
    {
        Task<T> GetAsync(string id);
        Task<T> CreateOrUpdateAsync(string id, T entity);
        Task<bool> DeleteAsync(string id);
    }
}
