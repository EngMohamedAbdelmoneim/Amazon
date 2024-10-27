using System.Runtime.Serialization;

namespace Amazon.Core.Entities.OrderAggregate
{
	public enum OrderStatus
	{
		[EnumMember(Value ="Pending")]
		Pending,

		[EnumMember(Value ="Processing")]
		Processing,

		[EnumMember(Value ="Shipped")]
		Shipped,
		
		[EnumMember(Value ="Delivered")]
		Delivered,

		[EnumMember(Value = "Cancelled")]
		Cancelled
	}
	public enum PaymentStatus
	{
		[EnumMember(Value ="Payment Pending")]
		paymentPending,

		[EnumMember(Value ="Payment Recieved")]
		PaymentRecieved,
		
		[EnumMember(Value ="Payment Failed")]
		PaymentFailed,
		
		[EnumMember(Value = "Refunded")]
		Refunded,
	}
	//public enum PaymentMethod
	//{
	//	[EnumMember(Value ="Cash On Delivery")]
	//	CashOnDelivery,

	//	[EnumMember(Value ="Online Payment")]
	//	OnlinePayment,
	//}
}
