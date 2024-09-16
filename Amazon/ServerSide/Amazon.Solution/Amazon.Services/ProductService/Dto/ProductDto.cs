using Amazon.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


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
		public IFormFile ImageFile { get; set; }
		public int QuantityInStock { get; set; }
		public int BrandId { get; set; }
		public int CategoryId { get; set; }
		public ICollection<ProductImages> Images { get; set; }
	}
}
