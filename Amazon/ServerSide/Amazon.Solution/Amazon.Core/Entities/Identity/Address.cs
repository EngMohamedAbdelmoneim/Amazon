using System.ComponentModel.DataAnnotations;

namespace Amazon.Core.Entities.Identity
{
	public class Address
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();
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

		public string AppUserId { get; set; }
		public virtual AppUser AppUser { get; set; }
	}
}