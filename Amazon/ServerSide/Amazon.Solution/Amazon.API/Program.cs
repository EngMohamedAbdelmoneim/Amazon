
using Amazon.Core.DBContext;
using Amazon.Services.ProductService.Dto;
using Amazon.Services.ProductService;
using Amazon.Services.ParentCategoryService;
using Amazon.Services.ParentCategoryService.Dto;
using Amazon.Services.CategoryServices;
using Amazon.Services.CategoryServices.Dto;
using Microsoft.EntityFrameworkCore;
using Amazone.Infrastructure.Interfaces;
using Amazone.Infrastructure.Repos;
using System.Text.Json.Serialization;
using Amazon.Core.Entities;

namespace Amazon.API
{
	public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            #region Connection String Configuration
            builder.Services.AddDbContext<AmazonDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            #region Register Services

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            #region ProductService
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddAutoMapper(typeof(ProductProfile));
            #endregion
            #region ParentCategoryService
            builder.Services.AddScoped<IParentCategoryService, ParentCategoryService>();
            builder.Services.AddAutoMapper(typeof(ParentCategoryProfile));
            #endregion
            #region CategoryService
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddAutoMapper(typeof(CategoryProfile));
            #endregion

            #endregion
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
                });
            });

			builder.Services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
