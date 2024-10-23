using Amazon.Services.ProductService.Dto;
using Amazon.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Amazon.Core.Entities;
using Amazon.Services.BrandService;
using Amazon.Services.CategoryServices;
using Amazon.Services.Utilities;
using Amazone.Infrastructure.Specification.ProductSpecifications;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Amazon.Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Amazon.API.Controllers
{

	public class ProductController : BaseController
	{

		private readonly IProductService _productService;
		private readonly IBrandService _brandService;
		private readonly ICategoryService _categoryService;

		public ProductController(IProductService productService, IBrandService brandService, ICategoryService categoryService)
		{
			_productService = productService;
			_brandService = brandService;
			_categoryService = categoryService;
		}

		[Authorize(Roles = "Seller")]
		[HttpPost]
		[ActionName("AddProduct")]
		public async Task<ActionResult<ProductToReturnDto>> AddProduct( ProductDto product)
		{
			var sellerEmail = User.FindFirstValue("Email");

			var brand = await _brandService.GetBrandByIdAsync(product.BrandId);
			var category = await _categoryService.GetCategoryByIdAsync(product.CategoryId);

			if (brand == null || category == null)
				return BadRequest();
			
			var result = await _productService.AddProduct(product,sellerEmail);
			return Ok(result);
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


			return Ok(product);
		}


		[Authorize(Roles = "Seller")]
		[HttpPut("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> UpdateProduct(int id,ProductDto product)
		{
			var sellerEmail = User.FindFirstValue("Email");


			var Exsistproduct = await _productService.GetProductByIdAsync(id);
			if (Exsistproduct == null)
				return Forbid();

			if (Exsistproduct.SellerEmail != sellerEmail)
				return Forbid();

			var brand = _brandService.GetBrandByIdAsync(product.BrandId);
			var category = _categoryService.GetCategoryByIdAsync(product.CategoryId);
			if (brand == null || category == null)
				return BadRequest();
			return Ok(await _productService.UpdateProduct(id, product));
		}

		[Authorize(Roles ="Seller")]
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetSellerProducts()
			=> Ok(await _productService.GetAllSellerProductsAsync(User.FindFirstValue("Email")));
		
		
		[Authorize(Roles ="Seller")]
		[HttpGet("{id}")]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetSellerProductById(int id)
		{
			var sellerEmail = User.FindFirstValue("Email");

			var result = await _productService.GetSellerProductByIdAsync(sellerEmail, id);
			if (result is null)
				return Forbid();

			 return Ok(result);
		}
		



		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetAllProducts()
			=> Ok(await _productService.GetAllProductsAsync());


		[Authorize(Roles = "Seller")]
		[HttpDelete("{id}")]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> DeleteProduct(int id)
		{
			var sellerEmail = User.FindFirstValue("Email");

			var product = await _productService.GetProductByIdAsync(id);
			if (product is null)
				return NotFound();

			if (product.SellerEmail != sellerEmail)
				return Forbid();

			return Ok(await _productService.DeleteProduct(id));

		}
	}

	
}

