using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.ReviewServices.Dto
{
	public class ReviewToReturnDto
	{
		public int Id { get; set; }
		public int Rating { get; set; }
		public string ReviewHeadLine { get; set; }
		public string ReviewText { get; set; }
		public DateTime ReviewDate { get; set; }
		public int ProductId { get; set; }
		public string AppUserName { get; set; }
		public string AppUserEmail { get; set; }
	}
}
