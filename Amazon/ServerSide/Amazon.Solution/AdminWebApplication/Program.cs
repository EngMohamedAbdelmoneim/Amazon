using Amazon.Core.DBContext;
using Amazon.Core.Entities;
using Amazon.Core.Entities.OrderAggregate;
using Amazon.Core.IdentityDb;
using Amazon.Services.BrandService;
using Amazon.Services.BrandService.Dto;
using Amazon.Services.CategoryServices;
using Amazon.Services.CategoryServices.Dto;
using Amazon.Services.OrderService;
using Amazon.Services.OrderService.OrderDto;
using Amazon.Services.ParentCategoryService;
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
            builder.Services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
            builder.Services.AddScoped<IGenericRepository<ParentCategory>, GenericRepository<ParentCategory>>();
            builder.Services.AddScoped<IGenericRepository<Order>, GenericRepository<Order>>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IParentCategoryService, ParentCategoryService>();
            //builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddAutoMapper(typeof(ProductProfile));
            builder.Services.AddAutoMapper(typeof(BrandProfile));
            builder.Services.AddAutoMapper(typeof(CategoryProfile));
            builder.Services.AddAutoMapper(typeof(OrderProfile));

            builder.Services.AddDbContext<AmazonDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
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
