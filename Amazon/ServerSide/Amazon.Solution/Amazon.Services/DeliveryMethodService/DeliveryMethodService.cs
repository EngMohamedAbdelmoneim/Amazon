using Amazon.Core.Entities.OrderAggregate;
using Amazon.Services.DeliveryMethodService.Dto;
using Amazone.Infrastructure.Interfaces;
using AutoMapper;

namespace Amazon.Services.DeliveryMethodService
{
	public class DeliveryMethodService : IDeliveryMethodService
	{
		private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
		private readonly IMapper _mapper;

		public DeliveryMethodService(IGenericRepository<DeliveryMethod> deliveryMethodRepo,IMapper mapper) 
		{
			_deliveryMethodRepo = deliveryMethodRepo;
			_mapper = mapper;
		}
		


		public async Task<IReadOnlyList<DeliveryMethodDto>> GetAllDeliveryMethodsAsync()
		{
			var deliveryMethods = await _deliveryMethodRepo.GetAllAsync();

			return  _mapper.Map<IReadOnlyList<DeliveryMethodDto>>(deliveryMethods);
		}

		public async Task<DeliveryMethodDto> GetDeliveryMethodByIdAsync(int id)
		{
			var deliveryMethod = await _deliveryMethodRepo.GetByIdAsync(id);
			return _mapper.Map<DeliveryMethodDto>(deliveryMethod);
		}

	
	}
}
