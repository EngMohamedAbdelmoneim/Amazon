using Amazon.Core.Entities.Identity;
using Amazon.Core.Entities.OrderAggregate;
using Amazon.Services.Utilities;
using AutoMapper;

namespace Amazon.Services.OrderService.OrderDto
{
	public class OrderProfile : Profile
	{
		public OrderProfile() 
		{
			CreateMap<Address, ShippingAddress>();

			CreateMap<Order, OrderToReturnDto>()
				.ForMember(d => d.DeliveredAt, o => o.MapFrom(s => s.DeliveryMethod.DeliveryTime))
				.ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Cost));

			CreateMap<OrderItem,OrderItemDto>()
				.ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
				.ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
				.ForMember(d => d.Brand, o => o.MapFrom(s => s.Product.Brand))
				.ForMember(d => d.Category, o => o.MapFrom(s => s.Product.Category))
				.ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.PictureUrl))
				.ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());

		}
	}
}
