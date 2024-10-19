using Amazon.Core.Entities.OrderAggregate;
using Amazon.Services.PaymentMethodService.Dto;

namespace Amazon.Services.OrderService.OrderDto
{
	public class OrderToReturnDto
	{
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
		public DateTimeOffset OrderDate { get; set; }
		public string OrderStatus { get; set; }
		public string PaymentStatus { get; set; }
		
		public PaymentMethodDto PaymentMethod { get; set; }
		public string ShippingAddressId { get; set; }
		public ShippingAddress ShippingAddress { get; set; }


		public ICollection<OrderItemDto> Items { get; set; } = new HashSet<OrderItemDto>(); // Navigational property Many

		public decimal SubTotal { get; set; }
		public decimal ShippingPrice { get; set; }
		public decimal Total { get; set; }
		public string DeliveredAt { get; set; }

		public string PaymentIntentId { get; set; }
	}
}
