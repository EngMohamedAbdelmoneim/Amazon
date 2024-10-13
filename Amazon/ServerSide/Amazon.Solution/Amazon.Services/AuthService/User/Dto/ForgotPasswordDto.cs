using System.ComponentModel.DataAnnotations;

namespace Amazon.Services.AuthService.User.Dto
{
	public class ForgotPasswordDto
	{

		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
