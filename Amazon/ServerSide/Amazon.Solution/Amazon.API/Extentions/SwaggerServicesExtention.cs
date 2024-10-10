using Microsoft.OpenApi.Models;

namespace Amazon.API.Extentions
{
	public static class SwaggerServicesExtention
	{
		public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
		{
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "AmazonAPI", Version = "v1" });

				var securityScheme = new OpenApiSecurityScheme
				{
					Description = "Jwt Authorization header using the Bearer scheme. Example \"Authorization:Bearer {token}\"",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				};
				c.AddSecurityDefinition("Bearer", securityScheme);


				var securityRequirement = new OpenApiSecurityRequirement
				{
					{securityScheme,new []{ "Bearer" } }
				};
				c.AddSecurityRequirement(securityRequirement);
			});
			return services;
		}
		public static WebApplication UseSwaggerMiddlewares(this WebApplication app)
		{
			app.UseSwagger();
			app.UseSwaggerUI();
			return app;
		}

	}
}
