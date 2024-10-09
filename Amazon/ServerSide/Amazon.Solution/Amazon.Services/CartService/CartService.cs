using Amazon.Core.Entities;
using Amazon.Services.CartService.Dto;
using Amazone.Infrastructure.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly IGenericCacheRepository<Cart> _cartRepository;
        private readonly IMapper _mapper;

        public CartService(IGenericCacheRepository<Cart> cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<CartDto> GetCartByIdAsync(string cartId)
        {
            var cart = await _cartRepository.GetAsync(cartId);
            return cart == null ? null : _mapper.Map<CartDto>(cart);
        }
        
        public async Task<CartDto> SetCartAsync(string cartId, CartDto cartDto)
        {
            var mappedCart = _mapper.Map<Cart>(cartDto);
            var newCart = await _cartRepository.CreateOrUpdateAsync(cartId, mappedCart);
            return newCart == null ? null : _mapper.Map<CartDto>(newCart);
        }

        public async Task<bool> RemoveCartAsync(string cartId)
        {
            return await _cartRepository.DeleteAsync(cartId);
        }
    }
}
