using Amazon.Services.ProductService.Dto;
using Amazon.Services.ProductService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.API.Controllers
{

	public class ProductController : BaseController
	{

		private readonly IProductService _productService;
		public ProductController(IProductService productService)
		{
			_productService = productService;
		}


		[HttpPost]
		[ActionName("AddProduct")]
		public async Task<ActionResult<ProductToReturnDto>> AddProduct(ProductDto product)
			=> await _productService.AddProduct(product);


		[HttpPut("id")]
		public async Task<ActionResult<ProductToReturnDto>> UpdateProduct(int id, ProductDto product)
			=> await _productService.UpdateProduct(id, product);


		[HttpGet]
		public async Task<IReadOnlyList<ProductToReturnDto>> GetAllProducts()
			=> await _productService.GetAllProductsAsync();

		[HttpGet("{id}")]
		public async Task<ProductToReturnDto> GetProductById(int id)
		  => await _productService.GetProductByIdAsync(id);


		[HttpDelete("{id}")]
		public async Task<IReadOnlyList<ProductToReturnDto>> DeleteProduct(int id)
		   => await _productService.DeleteProduct(id);
	}
}

