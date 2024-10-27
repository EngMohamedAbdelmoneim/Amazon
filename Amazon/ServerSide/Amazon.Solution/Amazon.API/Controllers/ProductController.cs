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
using Amazon.API.Errors;
using Microsoft.AspNetCore.Authentication;

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

		

		[HttpGet("GetAll")]
		public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery]ProductSpecParams specParams)
		{
			if (specParams.PageIndex < 0)
				return NotFound(new ApiResponse(404, "Page Not Found"));

			var products = await _productService.GetAllProductsAsync(specParams);

			if (products is null)
				return NotFound(new ApiResponse(404));

			return Ok(products);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var product = await _productService.GetProductByIdAsync(id);

			if (product is null)
				return NotFound(new ApiResponse(404));

			if (!product.IsAccepted)
				return NotFound(new ApiResponse(404));

			return Ok(product);
		}



	



		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetAllProducts()
			=> Ok(await _productService.GetAllProductsAsync());


	
	}

	
}

