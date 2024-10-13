using Microsoft.AspNetCore.Identity;

namespace Amazon.Core.Entities.Identity
{
	public class AppUser : IdentityUser
	{
		public string DisplayName { get; set; }
		public ICollection<Address> Addresses { get; set; } = new List<Address>();

		public string DefaultAddressId { get; set; }
	}
}
