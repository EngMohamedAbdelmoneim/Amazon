using Amazon.Core.Entities;
using Amazon.Services.ProductService.Dto;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.Utilities
{
	public class ProductUrlImageResolver : IValueResolver<Product, ProductToReturnDto, string>
	{

		private readonly IConfiguration _configuration;

		public ProductUrlImageResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		string IValueResolver<Product, ProductToReturnDto, string>.Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
				return _configuration["BaseUrl"] + source.PictureUrl;
			return null;
		}
	}
}
