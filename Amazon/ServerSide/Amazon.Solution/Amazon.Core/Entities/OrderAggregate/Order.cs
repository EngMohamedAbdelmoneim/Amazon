using System.ComponentModel.DataAnnotations.Schema;

namespace Amazon.Core.Entities.OrderAggregate
{
	public class Order :BaseEntity
	{

		public Order()
		{
			
		}

		public Order(string buyerEmail, string shippingAddressId,ShippingAddress shippingAddress, PaymentMethod paymentMethod, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
		{
			BuyerEmail = buyerEmail;
			ShippingAddressId = shippingAddressId;
			PaymentMethod = paymentMethod;
			DeliveryMethod = deliveryMethod;
			Items = items;
			SubTotal = subTotal;
			ShippingAddress = shippingAddress;
		}
		public Order(string buyerEmail, string shippingAddressId,ShippingAddress shippingAddress, PaymentMethod paymentMethod, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal,string paymentIntentId)
		{
			BuyerEmail = buyerEmail;
			ShippingAddressId = shippingAddressId;
			PaymentMethod = paymentMethod;
			DeliveryMethod = deliveryMethod;
			Items = items;
			SubTotal = subTotal;
			ShippingAddress = shippingAddress;
			PaymentIntentId = paymentIntentId;
		}


		public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

        //public ShippingAddress ShippingAddress { get; set; }

        public string ShippingAddressId { get; set; }
		[NotMapped]
        public virtual ShippingAddress ShippingAddress { get; set; } 

		public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.paymentPending;
        public virtual DeliveryMethod DeliveryMethod { get; set; } //Navigational Property One
        public virtual PaymentMethod PaymentMethod { get; set; } //Navigational Property One

        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>(); // Navigational property Many
		
		public decimal SubTotal { get; set; }

		public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;

		public string PaymentIntentId { get; set; }
    }
}
