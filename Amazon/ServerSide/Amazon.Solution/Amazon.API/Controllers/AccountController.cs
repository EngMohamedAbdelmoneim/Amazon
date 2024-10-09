using Amazon.Core.Entities.Identity;
using Amazon.Services.AuthService.User;
using Amazon.Services.AuthService.User.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Amazon.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly UserManager<AppUser> _userManager;

		public AccountController(IUserService userService,UserManager<AppUser> userManager)
        {
			_userService = userService;
			_userManager = userManager;
		}

        [HttpPost("Login")] //post  : /api/account/login
		public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
		{
			var user = await _userService.Login(loginDto);
			if (user == null)
				return Unauthorized();
			return Ok(user);
		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
		{
			var user = await _userService.Register(registerDto);
			if (user == null)
				return BadRequest("Already Registered");
			return Ok(user);
		}

		[Authorize]
		[HttpGet]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);

			var user = await _userManager.FindByEmailAsync(email);
			
			return Ok(new UserDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email
			});
		}  
	}
}
