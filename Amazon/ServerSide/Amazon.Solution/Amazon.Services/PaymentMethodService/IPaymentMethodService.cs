using Amazon.Services.PaymentMethodService.Dto;

namespace Amazon.Services.PaymentMethodService
{
	public interface IPaymentMethodService
	{
		Task<IReadOnlyList<PaymentMethodDto>> GetAllPaymentMethodsAsync();
		Task<PaymentMethodDto> GetPaymentMethodByIdAsync(int id);
	}
}
