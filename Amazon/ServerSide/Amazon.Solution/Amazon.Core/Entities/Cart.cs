using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Core.Entities
{
    public class Cart
    {
        public Cart()
        {
        }
        public Cart(string id)
        {
            Id = id;
        }

        [Required]
        public string Id { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
