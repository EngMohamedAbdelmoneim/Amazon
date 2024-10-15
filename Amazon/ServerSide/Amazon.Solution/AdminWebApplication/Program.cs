using Amazon.Core.DBContext;
using Amazon.Core.Entities;
using Amazon.Services.BrandService;
using Amazon.Services.BrandService.Dto;
using Amazon.Services.ProductService;
using Amazon.Services.ProductService.Dto;
using Amazone.Infrastructure.Interfaces;
using Amazone.Infrastructure.Repos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace AdminWebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IGenericRepository<Brand>, GenericRepository<Brand>>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddAutoMapper(typeof(ProductProfile));
            builder.Services.AddAutoMapper(typeof(BrandProfile));

            builder.Services.AddDbContext<AmazonDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            #region Accessing Amazon.API wwwroot

            var otherProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "../Amazon.API/wwwroot");
            var fileProvider = new PhysicalFileProvider(otherProjectPath);
            var options = new StaticFileOptions
            {
                FileProvider = fileProvider,
                RequestPath = "/Amazon.API"
            };

            app.UseStaticFiles(options);

            //app.UseStaticFiles(new StaticFileOptions
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine())
            //});

            #endregion

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
