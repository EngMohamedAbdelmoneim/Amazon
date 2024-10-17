using Amazon.Core.Entities.OrderAggregate;
using AutoMapper;

namespace Amazon.Services.PaymentMethodService.Dto
{
	public class PaymentMethodProfile : Profile
	{
        public PaymentMethodProfile()
        {
            CreateMap<PaymentMethod,PaymentMethodDto>();
        }
    }
}
