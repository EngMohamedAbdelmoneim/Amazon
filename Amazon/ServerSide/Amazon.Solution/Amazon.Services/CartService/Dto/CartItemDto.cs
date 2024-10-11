using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.CartService.Dto
{
    public class CartItemDto
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Category { get; set; }

        [Range(0.1, double.MaxValue)]
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
