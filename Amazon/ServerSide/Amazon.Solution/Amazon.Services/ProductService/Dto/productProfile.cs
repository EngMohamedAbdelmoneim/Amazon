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
				.ForMember(dest => dest.PictureUrl, options => options.MapFrom<ProductUrlImageResolver>());
		}
	}
}
