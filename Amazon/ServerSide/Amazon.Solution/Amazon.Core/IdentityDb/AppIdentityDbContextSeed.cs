using Amazon.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Core.IdentityDb
{
	public static class AppIdentityDbContextSeed
	{
		public static async Task SeedRolesAsync(RoleManager<IdentityRole> _roleManager)
		{
			if (_roleManager.Roles.Count() == 0)
			{
				var customerRole = new IdentityRole()
				{
					Name = "Customer"
				};
				await _roleManager.CreateAsync(customerRole);

				var adminRole = new IdentityRole()
				{
					Name = "Admin"
				};
				await _roleManager.CreateAsync(adminRole);
				
				var sellerRole = new IdentityRole()
				{
					Name = "Seller"
				};
				await _roleManager.CreateAsync(sellerRole);
			}
		}

		public static async Task SeedUsersAsync(UserManager<AppUser> _userManager)
		{

			if (_userManager.Users.Count() == 0)
			{
				var admin = new AppUser()
				{
					DisplayName = "AbdElRahman Saleh",
					Email = "Admin@gmail.com",
					EmailConfirmed = true,
					UserName = "AbdelRahmanSaleh",
					PhoneNumber = "01234567891",
					SellerName = "Amazon",
					IsActiveSeller = true
				};
				await _userManager.CreateAsync(admin, "Admin123?");
				await _userManager.AddToRoleAsync(admin, "Admin");
				await _userManager.AddToRoleAsync(admin, "Seller");

				var customer = new AppUser()
				{
					DisplayName = "Ali Mohammed",
					Email = "Customer@gmail.com",
					EmailConfirmed = true,
					UserName = "AliMohammed",
					PhoneNumber = "01234567892",
				};
				await _userManager.CreateAsync(customer, "Customer123?");
				await _userManager.AddToRoleAsync(customer, "Customer");
				
				var seller = new AppUser()
				{
					DisplayName = "Yousef Mohammed",
					Email = "Seller@gmail.com",
					EmailConfirmed = true,
					UserName = "YousefMohammed",
					PhoneNumber = "01234567892",
					SellerName	= "YM Store",
					IsActiveSeller = true
				};
				await _userManager.CreateAsync(seller, "Seller123?");
				await _userManager.AddToRoleAsync(seller, "Seller");
			}

		}
	}
}
