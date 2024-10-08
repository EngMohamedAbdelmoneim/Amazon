using Amazon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Infrastructure.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartAsync(string cartId);
        Task<Cart?> CreateOrUpdateCartAsync(Cart cart);
        Task<bool> DeleteCartAsync(string cartId);
    }
}
