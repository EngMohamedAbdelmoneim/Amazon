using Amazon.Services.BrandService;
using Amazon.Services.CategoryServices;
using Amazon.Services.ProductService;
using Amazon.Services.ProductService.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.API.Controllers
{
	
	//product Search controller
	public class SearchController : BaseController
	{
		private readonly IProductService _productService;
		private readonly ICategoryService _categoryService;
		private readonly IBrandService _brandService;

		public SearchController(IProductService productService,ICategoryService categoryService,IBrandService brandService)
		{
			_productService = productService;
			_categoryService = categoryService;
			_brandService = brandService;
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
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> SearchByCategoryId(int categoryId)
		{
			var cat = await _categoryService.GetCategoryByIdAsync(categoryId);
			
			if (cat == null)
				return NotFound();

			return Ok(await _productService.GetProductsByCategoryIdAsync(categoryId));
		
		}


		[HttpGet]
		public async Task<IReadOnlyList<ProductToReturnDto>> SearchByParentCategoryName(string? parentCategoryName)
			=> await _productService.GetProductsByParentCategoryNameAsync(parentCategoryName);

		[HttpGet("{categoryId}")]
		public async Task<IReadOnlyList<ProductToReturnDto>> SearchByParentCategoryId(int parentCategoryId)
			=> await _productService.GetProductsByParentCategoryIdAsync(parentCategoryId);

		[HttpGet("{brandId}")]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> SearchByBrandId(int brandId)
		{
			var brand = await _brandService.GetBrandByIdAsync(brandId);
			if (brand == null)
				return NotFound();

			return Ok(await _productService.GetProductsByBrandIdAsync(brandId));
		}

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


		[HttpGet]
		public async Task<IReadOnlyList<ProductToReturnDto>> SearchByStringPagination(string? str)
			=> await _productService.SearchByStringAsync(str);
	}
}
