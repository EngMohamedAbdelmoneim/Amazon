using Amazon.Services.ProductService.Dto;
using Amazon.Services.Utilities;
using Amazone.Infrastructure.Specification.ProductSpecifications;

namespace Amazon.Services.ProductService
{
	public interface IProductService
	{
		Task<IReadOnlyList<ProductToReturnDto>> GetAllProductsAsync();
		Task<Pagination<ProductToReturnDto>> GetAllProductsAsync(ProductSpecParams specParams);
		Task<ProductToReturnDto> GetProductByIdAsync(int productId);



		Task<ProductToReturnDto> GetSellerProductByIdAsync(string sellerEmail, int productId);
		Task<IReadOnlyList<ProductToReturnDto>> GetAllSellerProductsAsync(string sellerEmail);


		Task<IReadOnlyList<ProductToReturnDto>> GetProductsByBrandIdAsync(int id);
		Task<IReadOnlyList<ProductToReturnDto>> GetProductsByCategoryIdAsync(int id);
		Task<IReadOnlyList<ProductToReturnDto>> GetProductsByParentCategoryIdAsync(int id);


		Task<IReadOnlyList<ProductToReturnDto>> GetProductsByNameAsync(string name);
		Task<IReadOnlyList<ProductToReturnDto>> GetProductsByBrandNameAsync(string name);
		Task<IReadOnlyList<ProductToReturnDto>> GetProductsByCategoryNameAsync(string name);
		Task<IReadOnlyList<ProductToReturnDto>> GetProductsByParentCategoryNameAsync(string name);
		Task<IReadOnlyList<ProductToReturnDto>> SearchByStringAsync(string str);

		Task<IReadOnlyList<ProductToReturnDto>> GetProductsByCategoryIdAndNameAsync(string name, int? id);


		Task<ProductToReturnDto> AddProduct(ProductDto product,string sellerEmail);
		Task<ProductToReturnDto> UpdateProduct(int id, ProductDto product);
		Task<IReadOnlyList<ProductToReturnDto>> DeleteProduct(int id);
	}
}
