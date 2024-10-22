using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
namespace Amazon.Core.Entities
{
    public class Product : BaseEntity
    {

        public string Name { get; set; }

        public string Description { get; set; }
        
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

		public string PictureUrl { get; set; }

        [NotMapped]
		public IFormFile ImageFile { get; set; }

		public int QuantityInStock { get; set; }

        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }

        public virtual Brand Brand { get; set; }
        public int BrandId { get; set; }

        public string SellerName { get; set; }
        public string SellerEmail { get; set; }

        public virtual ICollection<ProductImages> Images { get; set; } = [];
        [NotMapped]
        public ICollection<IFormFile> ImagesFiles { get; set; }

        public virtual Discount Discount { get; set; }

        public virtual ICollection<Review> Reviews { get; set; } = [];

	}
}
