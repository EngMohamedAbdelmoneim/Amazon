using Amazon.Core.DBContext;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using StackExchange.Redis;
using Amazon.Core.IdentityDb;
using Microsoft.AspNetCore.Identity;
using Amazon.Core.Entities.Identity;
using Amazon.API.Extentions;

namespace Amazon.API
{
	public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container
            #region Configure Services
            builder.Services.AddControllers();
            builder.Services.AddSwaggerServices();
            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);

            #region Connection String Configuration
            builder.Services.AddDbContext<AmazonDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var options = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(options);
            });
			#endregion

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", policy =>
				{
					policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
				});
			});

			builder.Services.AddControllers().AddJsonOptions(x =>
			x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
			#endregion

			var app = builder.Build();
			#region Apply Migration
			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;
			var _IdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();
			var _dbContext = services.GetRequiredService<AmazonDbContext>();
			var loggerFactory = services.GetRequiredService<ILoggerFactory>();
			try
			{
                await _dbContext.Database.MigrateAsync(); //Update-Database

                await _IdentityDbContext.Database.MigrateAsync();

                var roleService = services.GetRequiredService<RoleManager<IdentityRole>>();
                await AppIdentityDbContextSeed.SeedRolesAsync(roleService);

                var userService = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUsersAsync(userService);
            }
			catch (Exception ex)
			{
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "An error has been occured during apply the migration");
			}
			#endregion

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
            {
				app.UseSwaggerMiddlewares();
			}
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
			app.UseStaticFiles();
            app.MapControllers();
            app.UseAuthentication();
            app.UseAuthorization();
            app.Run();
        }
    }
}
