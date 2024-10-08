using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Core.Entities
{
    public class WishlistItem
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }
        [Required]
        public string Category { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
