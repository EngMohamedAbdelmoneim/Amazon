using Amazon.Core.Entities;
using Amazon.Services.Utilities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
				.ForMember(dest => dest.CategoryName, options => options.MapFrom(b => b.Category.Name))
				.ForMember(dest => dest.BrandId, options => options.MapFrom(b => b.Brand.Id))
				.ForMember(dest => dest.CategoryId, options => options.MapFrom(b => b.Category.Id)).
				ForMember(dest => dest.ProductImages , options => options.MapFrom<ProductImagesUrlResolver>());
		}
	}
}
