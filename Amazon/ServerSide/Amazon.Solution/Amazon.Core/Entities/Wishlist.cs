using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Core.Entities
{
    public class Wishlist
    {
        public Wishlist()
        {
        }
        public Wishlist(string id)
        {
            Id = id;
        }

        [Required]
        public string Id { get; set; }
        public List<WishlistItem> Items { get; set; } = new List<WishlistItem>();
    }
}
