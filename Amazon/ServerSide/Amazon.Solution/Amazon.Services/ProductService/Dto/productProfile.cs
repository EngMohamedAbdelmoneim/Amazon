using Amazon.Core.Entities;
using Amazon.Services.Utilities;
using AutoMapper;

namespace Amazon.Services.ProductService.Dto
{
	public class ProductProfile : Profile
	{
		public ProductProfile()
		{
			CreateMap<ProductDto, Product>().ReverseMap();
			CreateMap<Product, ProductToReturnDto>()
				.ForMember(dest => dest.PictureUrl, options => options.MapFrom<ProductUrlImageResolver>())
				.ForMember(dest => dest.BrandName, options => options.MapFrom(b => b.Brand.Name))
				.ForMember(dest => dest.CategoryName, options => options.MapFrom(c => c.Category.Name))
				.ForMember(dest => dest.ProductImages , options => options.MapFrom<ProductImagesUrlResolver>());
		}
	}
}
