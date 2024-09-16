using Amazon.Services.ProductService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.ProductService
{
	public interface IProductService
	{
		Task<IReadOnlyList<ProductToReturnDto>> GetAllProductsAsync();
		Task<ProductToReturnDto> GetProductByIdAsync(int? id);
		Task<ProductToReturnDto> AddProduct(ProductDto product);
		Task<ProductToReturnDto> UpdateProduct(int id, ProductDto product);
		Task<IReadOnlyList<ProductToReturnDto>> DeleteProduct(int id);
	}
}
