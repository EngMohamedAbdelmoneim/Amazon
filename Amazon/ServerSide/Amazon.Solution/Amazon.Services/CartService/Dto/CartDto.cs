using Amazon.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.CartService.Dto
{
    public class CartDto
    {
        [Required]
        public string Id { get; set; }
        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
        public int? DeliveryMethodId { get; set; }
		public decimal ShippingPrice { get; set; }
		public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
    }
}
