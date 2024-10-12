using Amazon.Services.ProductService.Dto;
using Amazon.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Amazon.Core.Entities;
using Amazon.Services.BrandService;
using Amazon.Services.CategoryServices;
using Amazon.Services.Utilities;
using Amazone.Infrastructure.Specification.ProductSpecifications;

namespace Amazon.API.Controllers
{

	public class ProductController : BaseController
	{

		private readonly IProductService _productService;
		private readonly IBrandService _brandService;
		private readonly ICategoryService _categoryService;

		public ProductController(IProductService productService,IBrandService brandService , ICategoryService categoryService)
		{
			_productService = productService;
			_brandService = brandService;
			_categoryService = categoryService;
		}


		[HttpPost]
		[ActionName("AddProduct")]
		public async Task<ActionResult<ProductToReturnDto>> AddProduct(ProductDto product)
		{
			var brand = await _brandService.GetBrandByIdAsync(product.BrandId);
			var category =await _categoryService.GetCategoryByIdAsync(product.CategoryId);

			if (brand == null || category == null)
				return BadRequest();


			return Ok( await _productService.AddProduct(product));
		}

		[HttpGet("GetAll")]
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams specParams)
		{
			if (specParams.PageIndex < 0)
				return NotFound("Page Not Found");


			var products = await _productService.GetAllProductsAsync(specParams);

			if (products is null)
				return NotFound("Page Not Found");

			return Ok(products);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);

			if (product is null)
				return NotFound(); 


			return Ok(product); //Status Code 200
		}


		[HttpPut("id")]
		public async Task<ActionResult<ProductToReturnDto>> UpdateProduct(int id,ProductDto product)
		{
			var Exsistproduct = await _productService.GetProductByIdAsync(id);
			if (Exsistproduct == null)
				return NotFound();

			var brand = _brandService.GetBrandByIdAsync(product.BrandId);
			var category = _categoryService.GetCategoryByIdAsync(product.CategoryId);
			if (brand == null || category == null)
				return BadRequest();

			return Ok(await _productService.UpdateProduct(id, product));
		}

		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetAllProducts()
			=> Ok(await _productService.GetAllProductsAsync());


		[HttpDelete("{id}")]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> DeleteProduct(int id)
		{

			var proucts = await _productService.GetProductByIdAsync(id);
			if (proucts is null)
				return NotFound();

			return Ok(await _productService.DeleteProduct(id));

		}
	}

	
}

