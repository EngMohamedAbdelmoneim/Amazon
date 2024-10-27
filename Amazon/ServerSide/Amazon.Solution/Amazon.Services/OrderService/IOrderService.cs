using Amazon.Services.OrderService.OrderDto;

namespace Amazon.Services.OrderService
{
	public interface IOrderService
	{
		Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, string cartId, int paymentMethodId,int deliveryMethodId, string shippingAddressId);
		Task<IReadOnlyList<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail);

		Task<OrderToReturnDto> GetOrderByIdForUserAsync(int orderId, string buyerEmail);
		Task<OrderToReturnDto> CancelOrderAsync(int orderId, string buyerEmail);



	}
}
