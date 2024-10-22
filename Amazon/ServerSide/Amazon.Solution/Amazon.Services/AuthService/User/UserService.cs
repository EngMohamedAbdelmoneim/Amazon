using Amazon.Core.Entities.Identity;
using Amazon.Core.IdentityDb;
using Amazon.Services.AuthService.Token;
using Amazon.Services.AuthService.User.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Amazon.Services.AuthService.User
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ITokenService _tokenService;
		private readonly IMapper _mapper;
		private readonly AppIdentityDbContext _identityDb;

		public UserService(
			UserManager<AppUser> userManager,
			SignInManager<AppUser> signInManager,
			ITokenService tokenService,
			IMapper mapper,
			AppIdentityDbContext identityDb
			)
        {
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenService = tokenService;
			_mapper = mapper;
			_identityDb = identityDb;
		}

		public async Task<IdentityResult> Register(RegisterDto registerDto)
		{
			var user = await _userManager.FindByEmailAsync(registerDto.Email);


			if (user != null && await _userManager.IsInRoleAsync(user,"Customer"))
				return null;
			else if(user != null && !await _userManager.IsInRoleAsync(user, "Customer"))
				return await _userManager.AddToRoleAsync(user, "Customer");
			

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
			result = await _userManager.AddToRoleAsync(appUser, "Customer");

			if (!result.Succeeded)
				return null;

			return  result;
		}
		public async Task<IdentityResult> RegisterSeller(RegisterDto registerDto,string sellerName)
		{
			var user = await _userManager.FindByEmailAsync(registerDto.Email);


			if (user != null && await _userManager.IsInRoleAsync(user,"Seller"))
				return null;
			else if(user != null && !await _userManager.IsInRoleAsync(user, "Seller"))
			{
				user.SellerName = sellerName;
				await _userManager.UpdateAsync(user);
				return await _userManager.AddToRoleAsync(user, "Seller");
			}
			

			var appUser = new AppUser
			{
				DisplayName = registerDto.DisplayName,
				Email = registerDto.Email,
				UserName = registerDto.Email.Split('@')[0],
				PhoneNumber = registerDto.PhoneNumber,
				SellerName = sellerName,
			};

			var result = await _userManager.CreateAsync(appUser, registerDto.Password);

			if (!result.Succeeded)
				return null;
			result = await _userManager.AddToRoleAsync(appUser, "Seller");

			if (!result.Succeeded)
				return null;

			return  result;
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

		public async Task<IReadOnlyList<AddressToReturnDto>> AddAddressAsync(string userId, AddressDto addressDto)
		{

			var user = await _userManager.FindByIdAsync(userId);

			var address = _mapper.Map<Address>(addressDto);
			address.AppUserId = user.Id;

			user.Addresses.Add(address);
			await _userManager.UpdateAsync(user);

			var userAddresses = _mapper.Map<IReadOnlyList<AddressToReturnDto>>(user.Addresses);
			return userAddresses;
		}

		public async Task<AddressToReturnDto> EditAddressAsync(string userId, string addressId, AddressDto addressDto)
		{
			var user = await _userManager.FindByIdAsync(userId);
			var address = user.Addresses.FirstOrDefault(a => a.Id == addressId);
			if (address == null)
				return null;
			_mapper.Map(addressDto, address);
			await _userManager.UpdateAsync(user);

			return _mapper.Map<AddressToReturnDto>(address);
		}
		
		public async Task<IdentityResult> DeleteAddressAsync(string userId, string addressId)
		{
			var user = await _userManager.FindByIdAsync(userId);

			var address = user.Addresses.FirstOrDefault(a => a.Id == addressId);
			if (address == null)
				return null ;

			if (user.DefaultAddressId == addressId)
				user.DefaultAddressId = null; 

			user.Addresses.Remove(address);
			_identityDb.Addresses.Remove(address);
			 await _identityDb.SaveChangesAsync();

			var result = await _userManager.UpdateAsync(user);

			return result;
		}

		public async Task<object> SetDefaultAddress(string userId, string id)
		{
			var user = await _userManager.FindByIdAsync(userId);

			var address = user.Addresses.FirstOrDefault(a => a.Id == id);
			if (address == null)
				return null;
			user.DefaultAddressId = id;
			await _userManager.UpdateAsync(user);

			var result = new
			{
				Message = "Default address set successfully",
				DefaultAddress = _mapper.Map<AddressToReturnDto>(address),
				AllAddresses = _mapper.Map<IReadOnlyList<AddressToReturnDto>>(user.Addresses)
			};
			return result;
		}

		public async Task<IReadOnlyList<AddressToReturnDto>> GetUserAddresses(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			var UserAddresses = _mapper.Map<IReadOnlyList<AddressToReturnDto>>(user.Addresses);
			return UserAddresses;
		}

		public async Task<AddressToReturnDto> GetUserAddressById(string userId, string addressId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			var address = user.Addresses.FirstOrDefault(a => a.Id == addressId);
			if (address == null)
				return null;

			var addressDto = _mapper.Map<AddressToReturnDto>(address);

			return addressDto;
		}


	}
}
