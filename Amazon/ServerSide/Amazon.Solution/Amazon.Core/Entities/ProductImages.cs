using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Core.Entities
{
	public class ProductImages : BaseEntity
	{
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
        public string ImagePath { get; set; }
    }
}
