namespace Amazon.Services.Utilities.EmailSettings
{
	public interface IEmailService
	{
		Task SendEmail(Email email);
	}
}
