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
using Amazon.Services.OrderService;
using Amazon.Services.OrderService.OrderDto;
using Amazon.Services.DeliveryMethodService;
using Amazon.Services.DeliveryMethodService.Dto;
using Amazon.Services.PaymentMethodService;
using Amazon.Services.PaymentMethodService.Dto;
using Amazon.Services.ReviewServices;
using Amazon.Services.ReviewServices.Dto;

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

			#region OrderService
			services.AddScoped<IOrderService, OrderService>();
			services.AddAutoMapper(typeof(OrderProfile));

			#endregion

			#region DeliveryMethodService
			services.AddScoped<IDeliveryMethodService, DeliveryMethodService>();
			services.AddAutoMapper(typeof(DeliveryMethodProfile));
			#endregion

			#region PaymentMethodService
			services.AddScoped<IPaymentMethodService, PaymentMethodService>();
			services.AddAutoMapper(typeof(PaymentMethodProfile));
			#endregion

			#region ReviewService
			services.AddScoped<IReviewService, ReviewService>();
			services.AddAutoMapper(typeof(ReviewProfile));
			#endregion

			return services;
		}
	}
}
