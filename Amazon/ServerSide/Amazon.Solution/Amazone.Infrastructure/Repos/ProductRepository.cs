using Amazon.Core.DBContext;
using Amazon.Core.Entities;
using Amazone.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Amazone.Infrastructure.Repos
{
	public class ProductRepository : GenericRepository<Product>, IProductRepository
	{
		private readonly AmazonDbContext _context;

		public ProductRepository(AmazonDbContext context):base(context) 
        {
			_context = context;
		}


		public async Task<IReadOnlyList<Product>> SearchByProductNameAsync(string productName)
			=> await _context.Products.Where(p => p.Name.Contains(productName)).ToListAsync(); 

		public async Task<IReadOnlyList<Product>> SearchByBrandNameAsync(string brandName)
			=> await _context.Products.Include(p => p.Brand).Where(p => p.Brand.Name.Contains(brandName)).ToListAsync();
		public async Task<IReadOnlyList<Product>> SearchByBrandAsync(int brandId)
			=> await _context.Products.Where(p => p.BrandId == brandId).ToListAsync();

		public async Task<IReadOnlyList<Product>> SearchByCategoryAsync(int categoryId) 
			=> await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();

		public async Task<IReadOnlyList<Product>> SearchByCategoryNameAsync(string categoryName)
			=> await _context.Products.Include(p => p.Category).Where(p => p.Category.Name.Contains(categoryName)).ToListAsync();

		public async Task<IReadOnlyList<Product>> SearchByParentCategoryAsync(int parentCategoryId)
			=> await _context.Products.Include(p => p.Category).ThenInclude(c => c.ParentCategory).Where(p => p.Category.ParentCategoryId == parentCategoryId).ToListAsync();

		public async Task<IReadOnlyList<Product>> SearchByParentCategoryNameAsync(string parentCategoryName)
			=> await _context.Products.Include(p => p.Category).ThenInclude(c => c.ParentCategory).Where(p => p.Category.ParentCategory.Name.Contains(parentCategoryName)).ToListAsync();

		public async Task<IReadOnlyList<Product>> SearchByCategoryAndProductNameAsync(string productName,int? categoryId)
		{
			return await _context.Products.Include(p => p.Brand).Include(p => p.Category)
                                      .Where(p => p.Name.Contains(productName) ||
                                             p.Brand.Name.Contains(productName) ||
                                             p.Category.Name.Contains(productName)).Where(p => p.CategoryId == categoryId).ToListAsync();
        }

		public async Task<IReadOnlyList<Product>> SearchByStringAsync(string searchString) 
			=> await _context.Products.Include(p => p.Brand).Include(p => p.Category)
									  .Where(p => p.Name.Contains(searchString) ||
											 p.Brand.Name.Contains(searchString) ||
											 p.Category.Name.Contains(searchString)).ToListAsync();
	}
}
