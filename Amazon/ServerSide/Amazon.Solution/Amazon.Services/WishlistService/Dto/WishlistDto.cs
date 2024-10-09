using Amazon.Services.CartService.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.WishlistService.Dto
{
    public class WishlistDto
    {
        [Required]
        public string Id { get; set; }
        [MinLength(1)]
        public List<WishlistItemDto> Items { get; set; } = new List<WishlistItemDto>();
    }
}
