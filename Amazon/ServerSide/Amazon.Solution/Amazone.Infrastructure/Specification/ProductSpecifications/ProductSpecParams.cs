namespace Amazone.Infrastructure.Specification.ProductSpecifications
{
	public class ProductSpecParams
	{
        
        public string? Sort { get; set; }
		public int? BrandId { get; set; }
		public int? CategoryId { get; set; }
		public int? ParentCategoryId { get; set; }

		private const int maxPageSize = 10;
		
		private int pageSize = 8;
		
		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = value > 10 ? 10 : value; }
		}

		public int PageIndex { get; set; } = 1;

		private string? search;

		public string? Search
		{
			get { return search; }
			set { search = value?.ToLower(); }
		}
	}
}
