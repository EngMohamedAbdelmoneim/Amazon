using Amazon.Core.Entities;

namespace Amazone.Infrastructure.Specification.ProductSpecifications
{
	public class ProductWithBrandAndCategorySpecifications : BaseSpecification<Product>
	{
		public ProductWithBrandAndCategorySpecifications(ProductSpecParams specParams)
		: base(p =>
		   (string.IsNullOrEmpty(specParams.Search) ||(p.Name.ToLower().Contains(specParams.Search)
													 ||p.Brand.Name.ToLower().Contains(specParams.Search)
													 ||p.Category.Name.ToLower().Contains(specParams.Search)))
			&&
			(!specParams.BrandId.HasValue || p.BrandId == specParams.BrandId.Value)
			&&
			(!specParams.CategoryId.HasValue || p.CategoryId == specParams.CategoryId.Value)&&
			(!specParams.ParentCategoryId.HasValue || p.Category.ParentCategoryId == specParams.ParentCategoryId.Value))
		{
			AddIncludes();

			if (!string.IsNullOrWhiteSpace(specParams.Sort))
			{
				switch (specParams.Sort)
				{
					case "priceAsc":
						AddOrderBy(p => p.Price);
						break;
					case "priceDesc":
						AddOrderByDesc(p => p.Price);
						break;
					default:
						AddOrderBy(p => p.Name);
						break;
				}
			}
			else
			{
				AddOrderBy(p => p.Name);
			}
			ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize, specParams.PageSize);
		}

		public ProductWithBrandAndCategorySpecifications(int id)
			: base(P => P.Id == id)
		{
			AddIncludes();
		}


		private void AddIncludes()
		{
			Includes.Add(P => P.Brand);
			Includes.Add(P => P.Category);
		}

	}
}
