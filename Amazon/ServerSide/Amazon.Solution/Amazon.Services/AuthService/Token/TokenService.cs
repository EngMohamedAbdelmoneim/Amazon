using Amazon.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Amazon.Services.AuthService.Token
{
	public class TokenService :ITokenService
	{
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
		{
			//Private Claims
			var authClaims = new List<Claim>()
			{
				new Claim("Name",user.UserName),
				new Claim("Sid",user.Id),
				new Claim("Email",user.Email)
			};
			var userRoles = await userManager.GetRolesAsync(user);

			foreach (var role in userRoles)
				authClaims.Add(new Claim("Role", role));


			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

			var token = new JwtSecurityToken(
				audience: _configuration["JWT:ValidAudience"],
				issuer: _configuration["JWT:ValidIssuer"],
				expires: DateTime.UtcNow.AddDays(double.Parse(_configuration["JWT:DurationInDays"])),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
