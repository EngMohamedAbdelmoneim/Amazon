using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.ProductService.Dto
{
	public class ItemEarningsDto
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public decimal Earnings { get; set; }
		public int QuantitySold { get; set; }
	}
}
