using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace Amazon.Services.ProductService.Dto
{
	public class ProductDto
	{
	
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public IFormFile ImageFile { get; set; }
		public int QuantityInStock { get; set; }
		public int BrandId { get; set; }
		public int CategoryId { get; set; }
		public DiscountDto Discount { get; set; }
		public ICollection<IFormFile> ImagesFiles { get; set; }
    }
}
