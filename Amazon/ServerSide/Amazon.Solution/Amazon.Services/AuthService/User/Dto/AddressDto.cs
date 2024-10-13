using System.ComponentModel.DataAnnotations;

namespace Amazon.Services.AuthService.User.Dto
{
	public class AddressDto
	{
		//public string Id { get; set; }
		[Required]
		public string Country { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
		[Required]
		public string StreetAddress { get; set; }
		[Required]
		public string BuildingName { get; set; }
		[Required]
		public string City { get; set; }
		[Required]
		public string District { get; set; }
		[Required]
		public string Governorate { get; set; }
		public string NearestLandMark { get; set; }
		
	}
}
