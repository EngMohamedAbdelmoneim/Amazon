using Amazon.Core.Entities;
using Amazon.Services.CartService.Dto;
using Amazon.Services.WishlistService.Dto;
using Amazone.Infrastructure.Interfaces;
using Amazone.Infrastructure.Repos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.WishlistService
{
    public class WishlistService : IWishlistService
    {
        private readonly IGenericCacheRepository<Wishlist> _wishlistRepo;
        private readonly IMapper _mapper;

        public WishlistService(IGenericCacheRepository<Wishlist> wishlistRepository, IMapper mapper)
        {
            _wishlistRepo = wishlistRepository;
            _mapper = mapper;
        }
        public async Task<WishlistDto> GetWishlistByIdAsync(string wishlistId)
        {
            var wishlist = await _wishlistRepo.GetAsync(wishlistId);
            return wishlist == null ? null : _mapper.Map<WishlistDto>(wishlist);
        }

        public async Task<WishlistDto> SetWishlistAsync(string wishlistId, WishlistDto wishlistDto)
        {
            var mappedWishlist = _mapper.Map<Wishlist>(wishlistDto);
            var newWishlist = await _wishlistRepo.CreateOrUpdateAsync(wishlistId, mappedWishlist);
            return newWishlist == null ? null : _mapper.Map<WishlistDto>(newWishlist);
        }

        public async Task<bool> RemoveWishlistAsync(string wishlistId)
        {
            return await _wishlistRepo.DeleteAsync(wishlistId);
        }
    }
}
