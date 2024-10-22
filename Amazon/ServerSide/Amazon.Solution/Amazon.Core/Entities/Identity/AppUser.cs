using Microsoft.AspNetCore.Identity;

namespace Amazon.Core.Entities.Identity
{
	public class AppUser : IdentityUser
	{
		public string DisplayName { get; set; }
		public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

		public string SellerName { get; set; }

		public string DefaultAddressId { get; set; }
	}
}
