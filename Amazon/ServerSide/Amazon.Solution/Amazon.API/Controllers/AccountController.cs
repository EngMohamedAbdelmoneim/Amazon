using Amazon.API.Extentions;
using Amazon.Core.Entities.Identity;
using Amazon.Services.AuthService.Token;
using Amazon.Services.AuthService.User;
using Amazon.Services.AuthService.User.Dto;
using Amazon.Services.Utilities.EmailSettings;
using Amazon.Services.Utilities.EmailSettings.EmailBodyGenerator;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Amazon.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IEmailService _emailService;
		private readonly ITokenService _tokenService;
		private readonly UserManager<AppUser> _userManager;
		private readonly IMapper _mapper;

		public AccountController(IUserService userService,
						   ITokenService tokenService,
						   IEmailService emailService,
						   UserManager<AppUser> userManager,
						   IMapper mapper)
        {
			_userService = userService;
			_tokenService = tokenService;
			_emailService = emailService;
			_userManager = userManager;
			_mapper = mapper;
		}


		[HttpPost("register")]
		public async Task<ActionResult> Register(RegisterDto registerDto)
		{
			var result = await _userService.Register(registerDto);
			if (result is null)
				return BadRequest("Already Registered");

			if (result.Succeeded)
			{
				var user = await _userManager.FindByEmailAsync(registerDto.Email);
				if (user is null)
					return NotFound();

				return Ok(await SendConfirmationEmail(user));
			}
			else
				return BadRequest("Register Failed");
		}

		
		[HttpPost("Login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
		{
			var user = await _userService.Login(loginDto);
			if (user == null)
				return Unauthorized();
			return Ok(user);
		}


		[ApiExplorerSettings(IgnoreApi = true)]
		[HttpPost("sendconfirmationemail")]
		public async Task<IActionResult> SendConfirmationEmail(AppUser user)
		{
			var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			var confirmationLink = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, token = token }, Request.Scheme);
			var email = new Email
			{
				Title = "Confirm Your Email Address",
				Body = EmailBodyGenerator.ConfirmationEmailBodyGenerator(user, confirmationLink),
				To = user.Email,
				IsHtml = true
			};
			await _emailService.SendEmail(email);
			return Ok("Confirmation Email send");
		}


		[HttpGet("confirmemail")]
		public async Task<IActionResult> ConfirmEmail(string userId, string token)
		{
			if (userId == null || token == null)
			{
				return BadRequest("Invalid confirmation link.");
			}
			var user = await _userManager.FindByIdAsync(userId);

			if (user == null)
				return BadRequest("Invalid User");

			if (!user.EmailConfirmed)
			{
				var result = await _userManager.ConfirmEmailAsync(user, token);

				if (result.Succeeded)
				{
					return Ok(new UserDto
					{
						DisplayName = user.DisplayName,
						Email = user.Email,
						Token = await _tokenService.CreateTokenAsync(user, _userManager)
					});
				}
			}
			else
				return Ok("Email Already Confirmed");

			return BadRequest("Invalid or expired confirmation link.");
		}

		
		[HttpPost("ForgotPassword")]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user is null)
			{
				return BadRequest("Invalid request.");
			}

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);

			var resetLink = Url.Action("ResetPassword", "Account", new { token = token, userId = user.Id }, Request.Scheme);

			var email = new Email
			{
				Title = "Reset Your Password",
				Body = EmailBodyGenerator.ResetPasswordEmailBodyGenerator(user, resetLink),  // Your custom email body generator
				To = user.Email,
				IsHtml = true
			};
			await _emailService.SendEmail(email);

			return Ok("Password reset link has been sent to your email.");
		}

		
		[HttpPost("ResetPassword")]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model, [FromQuery] string userId, [FromQuery] string token)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
			{
				return BadRequest("Invalid request.");
			}

			var result = await _userManager.ResetPasswordAsync(user, token, model.Password);

			if (result.Succeeded)
			{
				return Ok("Password has been reset successfully.");
			}

			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
			return BadRequest(ModelState);
		}


		[Authorize]
		[HttpGet]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var email = User.FindFirstValue("Email");

			var user = await _userManager.FindByEmailAsync(email);
			
			return Ok(new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email
			});
		}


		[Authorize]
		[HttpPost("addAddress")]
		public async Task<ActionResult<IReadOnlyList<AddressDto>>> AddAddress([FromBody] AddressDto addressDto)
		{
			var user = await _userManager.FindUserWithAddressAsync(User);
			var result =  await _userService.AddAddressAsync(user.Id, addressDto);
			if (result is null)
				return BadRequest();
			return Ok(result);
		}


		[Authorize]
		[HttpPut("editAddress/{addressId}")]
		public async Task<IActionResult> EditAddress(string addressId, [FromBody] AddressDto addressDto)
		{
			var user = await _userManager.FindUserWithAddressAsync(User);

			var result = await _userService.EditAddressAsync(user.Id,addressId,addressDto);
			if (result is null)
				return NotFound("Address not found");

			return Ok(result);
		}


		[Authorize]
		[HttpDelete("deleteAddress/{addressId}")]
		public async Task<IActionResult> DeleteAddress(string addressId)
		{
			var user = await _userManager.FindUserWithAddressAsync(User);
			
			var result = await _userService.DeleteAddressAsync(user.Id, addressId);
			if (result is null)
				return NotFound("Address not found");
			if (!result.Succeeded)
				return BadRequest("Failed to delete the address");

			return Ok("Address Deleted Successfully");
		}

		
		[Authorize]
		[HttpPost("setDefaultAddress")]
		public async Task<IActionResult> SetDefaultAddress(string id)
		{
			var user = await _userManager.FindUserWithAddressAsync(User);

			var result = await _userService.SetDefaultAddress(user.Id, id);

			if (result is null)
				return BadRequest();

			return Ok(result);
		}

		
		[Authorize]
		[HttpGet("getAddresses")]
		public async Task<IActionResult> GetAddresses()
		{
			var user = await _userManager.FindUserWithAddressAsync(User);

			var result = await _userService.GetUserAddresses(user.Id);

			if (result.IsNullOrEmpty())
				return Ok("No Addresses");

			return Ok(result);
		}

		
		[Authorize]
		[HttpGet("getAddress/{addressId}")]
		public async Task<IActionResult> GetAddress(string addressId)
		{
			var user = await _userManager.FindUserWithAddressAsync(User);

			var result = await _userService.GetUserAddressById(user.Id, addressId);

			if (result is null)
				return NotFound("Address not found");

			return Ok(result);
		}

	}
}
