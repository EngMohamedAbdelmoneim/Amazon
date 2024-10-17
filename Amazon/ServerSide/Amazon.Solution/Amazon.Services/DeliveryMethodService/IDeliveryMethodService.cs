using Amazon.Services.DeliveryMethodService.Dto;

namespace Amazon.Services.DeliveryMethodService
{
	public interface IDeliveryMethodService
	{
		Task<IReadOnlyList<DeliveryMethodDto>> GetAllDeliveryMethodsAsync();
		Task<DeliveryMethodDto> GetDeliveryMethodByIdAsync(int id);
	}
}
