using Amazon.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.ProductService.Dto
{
	public class ProductToReturnDto
	{
        public string Name { get; set; }

		public string Description { get; set; }

		[Column(TypeName = "money")]
		public decimal Price { get; set; }

		public string PictureUrl { get; set; }

		public int QuantityInStock { get; set; }

		public Category Category { get; set; }
		public int CategoryId { get; set; }
		public virtual Brand Brand { get; set; }
		public int BrandId { get; set; }
		public ICollection<ProductImages> Images { get; set; }
	}
}
