using Amazon.Core.Entities.OrderAggregate;
using Amazon.Services.PaymentMethodService.Dto;
using Amazone.Infrastructure.Interfaces;
using AutoMapper;

namespace Amazon.Services.PaymentMethodService
{
	public class PaymentMethodService : IPaymentMethodService
	{
		private readonly IGenericRepository<PaymentMethod> _paymentMethodRepo;
		private readonly IMapper _mapper;

		public PaymentMethodService(IGenericRepository<PaymentMethod> paymentMethodRepo, IMapper mapper)
		{
			_paymentMethodRepo = paymentMethodRepo;
			_mapper = mapper;
		}

		public async Task<IReadOnlyList<PaymentMethodDto>> GetAllPaymentMethodsAsync()
		{
			var paymentMethods = await _paymentMethodRepo.GetAllAsync();
			return _mapper.Map<IReadOnlyList<PaymentMethodDto>>(paymentMethods);
		}

		public async Task<PaymentMethodDto> GetPaymentMethodByIdAsync(int id)
		{
			var paymentMethod = await _paymentMethodRepo.GetByIdAsync(id);
			return _mapper.Map<PaymentMethodDto>(paymentMethod);
		}
	}
}
