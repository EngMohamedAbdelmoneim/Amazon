using Amazon.Services.ProductService;
using Amazon.Services.ProductService.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.API.Controllers
{
	
	//product Search controller
	public class SearchController : BaseController
	{
		private readonly IProductService _productService;

		public SearchController(IProductService productService)
		{
			_productService = productService;
		}


		[HttpGet]
		public async Task<IReadOnlyList<ProductToReturnDto>> SearchByProductName(string? productName)
			=> await _productService.GetProductsByNameAsync(productName);

		[HttpGet]
		public async Task<IReadOnlyList<ProductToReturnDto>> SearchByBrandName(string? brandName)
			=> await _productService.GetProductsByBrandNameAsync(brandName);

		[HttpGet]
		public async Task<IReadOnlyList<ProductToReturnDto>> SearchByCategoryName(string? categoryName)
			=> await _productService.GetProductsByCategoryNameAsync(categoryName);

		[HttpGet("{categoryId}")]
		public async Task<IReadOnlyList<ProductToReturnDto>> SearchByCategoryId(int categoryId)
			=> await _productService.GetProductsByCategoryIdAsync(categoryId);

		[HttpGet("{brandId}")]
		public async Task<IReadOnlyList<ProductToReturnDto>> SearchByBrandId(int brandId)
			=> await _productService.GetProductsByBrandIdAsync(brandId);

		[HttpGet]
		public async Task<IReadOnlyList<ProductToReturnDto>> SearchByProductNameAndCategoryId(string? productName, int? categoryId)
			=> await _productService.GetProductsByCategoryIdAndNameAsync(productName, categoryId);

		
		/// <summary>
		/// search End Point that take a string  and return the search result if the product name or category or brand contains that string
		/// </summary>
		/// <param name="Search Keyword"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IReadOnlyList<ProductToReturnDto>> SearchByString(string? str)
			=> await _productService.SearchByStringAsync(str);
	}
}
