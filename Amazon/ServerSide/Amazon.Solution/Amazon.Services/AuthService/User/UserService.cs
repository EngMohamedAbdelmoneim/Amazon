using Amazon.Core.Entities.Identity;
using Amazon.Services.AuthService.Token;
using Amazon.Services.AuthService.User.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.Services.AuthService.User
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenService _tokenService;

		public UserService(
			UserManager<AppUser> userManager,
			SignInManager<AppUser> signInManager,
			ITokenService tokenService)
        {
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
		}

		public async Task<UserDto> Login(LoginDto loginDto)
		{
			var user = await _userManager.FindByEmailAsync(loginDto.Email);
			if (user == null)
				return null;

			var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
			if (!result.Succeeded)
				return null;

			return new UserDto
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await _tokenService.CreateTokenAsync(user, _userManager)
			};
		}

		public async Task<UserDto> Register(RegisterDto registerDto)
		{
			var user = await _userManager.FindByEmailAsync(registerDto.Email);
			if (user != null)
				return null;

			var appUser = new AppUser
			{
				DisplayName = registerDto.DisplayName,
				Email = registerDto.Email,
				UserName = registerDto.Email.Split('@')[0],
				PhoneNumber = registerDto.PhoneNumber,
			};
			var result = await _userManager.CreateAsync(appUser, registerDto.Password);

			if (!result.Succeeded)
				return null;

			await _userManager.AddToRoleAsync(appUser, "Customer");

			return new UserDto
			{
				DisplayName = appUser.DisplayName,
				Email = appUser.Email,
				Token = await _tokenService.CreateTokenAsync(appUser, _userManager)
			};
		}
	}
}
