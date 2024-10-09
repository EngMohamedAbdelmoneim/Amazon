using Amazon.Services.BrandService.Dto;
using Amazon.Services.BrandService;
using Amazon.Services.CartService.Dto;
using Amazon.Services.CartService;
using Amazon.Services.CategoryServices.Dto;
using Amazon.Services.CategoryServices;
using Amazon.Services.ParentCategoryService.Dto;
using Amazon.Services.ParentCategoryService;
using Amazon.Services.ProductService.Dto;
using Amazon.Services.ProductService;
using Amazon.Services.WishlistService.Dto;
using Amazon.Services.WishlistService;
using Amazone.Infrastructure.Interfaces;
using Amazone.Infrastructure.Repos;

namespace Amazon.API.Extentions
{
	public static class ApplicationServicesExtention
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped(typeof(IGenericCacheRepository<>), typeof(GenericCacheRepository<>));

			#region ProductService
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IProductService, ProductService>();
			services.AddAutoMapper(typeof(ProductProfile));
			#endregion

			#region ParentCategoryService
			services.AddScoped<IParentCategoryService, ParentCategoryService>();
			services.AddAutoMapper(typeof(ParentCategoryProfile));
			#endregion

			#region CategoryService
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddAutoMapper(typeof(CategoryProfile));
			#endregion

			#region BrandService
			services.AddScoped<IBrandService, BrandService>();
			services.AddAutoMapper(typeof(BrandProfile));
			#endregion


			#region CartService
			services.AddScoped<ICartService, CartService>();
			services.AddAutoMapper(typeof(CartProfile));
			#endregion

			#region WishlistService
			services.AddScoped<IWishlistService, WishlistService>();
			services.AddAutoMapper(typeof(WishlistProfile));
			#endregion

			return services;
		}
	}
}
