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


	public class ProductImagesUrlResolver : IValueResolver<Product, ProductToReturnDto, ICollection<string>>
	{
		private readonly IConfiguration _configuration;

		public ProductImagesUrlResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public ICollection<string> Resolve(Product source, ProductToReturnDto destination, ICollection<string> destMember, ResolutionContext context)
		{
			if (source.Images != null && source.Images.Any())
			{
				return source.Images.Select(image => _configuration["BaseUrl"] + image.ImagePath).ToList();
			}
			return new List<string>();
		}
	}
}
