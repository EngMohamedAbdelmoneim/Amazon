using Amazon.Core.Entities;
using Amazon.Services.CartService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.CartService
{
    public interface ICartService
    {
        Task<CartDto> GetCartByIdAsync(string cartId);
        Task<CartDto> SetCartAsync(string cartId, CartDto cartDto);
        Task<bool> RemoveCartAsync(string cartId);
    }
}
