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

		Task<ProductToReturnDto> GetProductByIdAsync(int id);
		Task<IReadOnlyList<ProductToReturnDto>> GetProductsByBrandIdAsync(int id);
		Task<IReadOnlyList<ProductToReturnDto>> GetProductsByCategoryIdAsync(int id);

		Task<IReadOnlyList<ProductToReturnDto>> GetProductsByNameAsync(string name);
		Task<IReadOnlyList<ProductToReturnDto>> GetProductsByBrandNameAsync(string name);
		Task<IReadOnlyList<ProductToReturnDto>> GetProductsByCategoryNameAsync(string name);
		Task<IReadOnlyList<ProductToReturnDto>> SearchByStringAsync(string str);

		Task<IReadOnlyList<ProductToReturnDto>> GetProductsByCategoryIdAndNameAsync(string name, int? id);

		Task<ProductToReturnDto> AddProduct(ProductDto product);
		Task<ProductToReturnDto> UpdateProduct(int id, ProductDto product);
		Task<IReadOnlyList<ProductToReturnDto>> DeleteProduct(int id);
	}
}
