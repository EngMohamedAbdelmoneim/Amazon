using Amazon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Infrastructure.Interfaces
{
	public interface IProductRepository : IGenericRepository<Product>
	{
		Task<IReadOnlyList<Product>> SearchByProductNameAsync(string productName);
		
		Task<IReadOnlyList<Product>> SearchByBrandAsync(int brandId);
		Task<IReadOnlyList<Product>> SearchByBrandNameAsync(string brandName);

		Task<IReadOnlyList<Product>> SearchByCategoryAsync(int categoryId);
		Task<IReadOnlyList<Product>> SearchByCategoryNameAsync(string categoryName);
		
		Task<IReadOnlyList<Product>> SearchByCategoryAndProductNameAsync(string productName,int? categoryId);

		Task<IReadOnlyList<Product>> SearchByStringAsync(string searchString);

	}
}

