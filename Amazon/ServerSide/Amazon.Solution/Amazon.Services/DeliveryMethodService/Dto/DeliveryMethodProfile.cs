using Amazon.Core.Entities.OrderAggregate;
using AutoMapper;

namespace Amazon.Services.DeliveryMethodService.Dto
{
	public class DeliveryMethodProfile : Profile
	{
		public DeliveryMethodProfile() 
		{
			CreateMap<DeliveryMethod, DeliveryMethodDto>();		
		}
	}
}
