using Amazon.Core.Entities.Identity;
using Amazon.Services.AuthService.User.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Amazon.Services.AuthService.User
{
	public interface IUserService
	{
		Task<IdentityResult> Register(RegisterDto registerDto);
		Task<UserDto> Login(LoginDto logInDto);
		Task<IReadOnlyList<AddressToReturnDto>> AddAddressAsync(string userEmail, AddressDto addressDto);
		Task<AddressToReturnDto> EditAddressAsync(string userId, string addressId ,AddressDto addressDto);
		Task<IdentityResult> DeleteAddressAsync(string userId, string addressId);
		Task<object> SetDefaultAddress(string userId, string id);
		Task<AddressToReturnDto> GetUserAddressById(string userId,string addressId);
		Task<IReadOnlyList<AddressToReturnDto>> GetUserAddresses(string userId);


	}
}
