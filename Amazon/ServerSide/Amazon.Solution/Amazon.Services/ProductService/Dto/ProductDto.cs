using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace Amazon.Services.ProductService.Dto
{
	public class ProductDto
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public IFormFile ImageFile { get; set; }
		[Required]
		public int QuantityInStock { get; set; }
		[Required]
		public int BrandId { get; set; }
		[Required]
		public int CategoryId { get; set; }
		public DiscountDto Discount { get; set; }
		public ICollection<IFormFile> ImagesFiles { get; set; }
    }
}
