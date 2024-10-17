using System.ComponentModel.DataAnnotations;

namespace Amazon.Services.OrderService.OrderDto
{
	public class OrderDto
	{
		[Required]
		public string CartId { get; set; }
		[Required]
		public int DeliveryMethodId { get; set; }
		[Required]
		public int PaymentMethodId { get; set; }	
		[Required]
		public string ShippingAddressId { get; set; }
	}
}
