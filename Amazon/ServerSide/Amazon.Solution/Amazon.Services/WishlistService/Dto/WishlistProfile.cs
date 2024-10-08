using Amazon.Core.Entities;
using Amazon.Services.CartService.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.WishlistService.Dto
{
    public class WishlistProfile : Profile
    {
        public WishlistProfile()
        {
            CreateMap<Wishlist, WishlistDto>().ReverseMap();
            CreateMap<WishlistItem, WishlistItemDto>().ReverseMap();
        }
    }
}
