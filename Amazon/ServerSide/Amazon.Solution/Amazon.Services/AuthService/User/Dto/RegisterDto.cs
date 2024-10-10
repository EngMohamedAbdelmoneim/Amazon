using System.ComponentModel.DataAnnotations;

namespace Amazon.Services.AuthService.User.Dto
{
	public class RegisterDto
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string DisplayName { get; set; }

		[RegularExpression(@"(010|011|012|015)\d{8}", ErrorMessage = "Invalid Phone Number")]
		[Required]
		[MaxLength(11, ErrorMessage = "Mobile number must be 11 digits")]
		[MinLength(11, ErrorMessage = "Mobile number must be 11 digits")]
		public string PhoneNumber { get; set; }
		[Required]
		[RegularExpression("(?=^.{6,50}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^A-Za-z0-9])(?!.*\\s).*$"
			, ErrorMessage = "This Password must conatains 1 upperCasr , 1 LowerCase , 1 Number ,1 non alphanumeric and at least 6 characters ")]
		public string Password { get; set; }
		[Compare("Password", ErrorMessage = "Password Don't Match")]
		public string ConfirmPassword { get; set; }
	}
}
