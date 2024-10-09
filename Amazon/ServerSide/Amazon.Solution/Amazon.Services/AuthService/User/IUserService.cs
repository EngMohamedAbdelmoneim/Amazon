using Amazon.Services.AuthService.User.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.AuthService.User
{
	public interface IUserService
	{
		Task<UserDto> Register(RegisterDto registerDto);
		Task<UserDto> Login(LoginDto logInDto);

	}
}
