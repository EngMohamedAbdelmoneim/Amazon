using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Core.Entities.Identity
{
	public class AppUser : IdentityUser
	{
		public string DisplayName { get; set; }
		public ICollection<Address> Addresses { get; set; } = new List<Address>();
	}
}
