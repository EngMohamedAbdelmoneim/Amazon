using Amazon.Core.Entities;

namespace Amazone.Infrastructure.Specification.ProductSpecifications
{
	public class ProductWithFilterationForCountSpecification : BaseSpecification<Product>
	{
		public ProductWithFilterationForCountSpecification(ProductSpecParams specParams)
		: base(p =>
			(string.IsNullOrEmpty(specParams.Search) || (p.Name.ToLower().Contains(specParams.Search)
												 || p.Brand.Name.ToLower().Contains(specParams.Search)
												 || p.Category.Name.ToLower().Contains(specParams.Search)))
			&&
			(!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId.Value)
			&&
			(!specParams.CategoryId.HasValue || p.CategoryId == specParams.CategoryId.Value) &&
			(!specParams.ParentCategoryId.HasValue || p.Category.ParentCategoryId == specParams.ParentCategoryId.Value))
		{

		}
	}
}
