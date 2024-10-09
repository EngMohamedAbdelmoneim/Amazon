using Amazon.Core.Entities.Identity;
using Amazon.Core.IdentityDb;
using Amazon.Services.AuthService.Token;
using Amazon.Services.AuthService.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Amazon.API.Extentions
{
	public static class IdentityServicesExtention
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration configuration)
		{
			services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<ITokenService, TokenService>();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				//Configure Authentication Handler For Bearer Schema
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateAudience = true,
					ValidAudience = configuration["JWT:ValidAudience"],
					ValidateIssuer = true,
					ValidIssuer = configuration["JWT:ValidIssuer"],
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])),
					ValidateLifetime = true,
					ClockSkew = TimeSpan.FromDays(double.Parse(configuration["JWT:DurationInDays"]))
				};
			});

			return services;
		}
	}
}
