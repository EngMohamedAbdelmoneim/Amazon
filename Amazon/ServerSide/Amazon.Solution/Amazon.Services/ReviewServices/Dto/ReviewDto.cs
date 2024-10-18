using Amazon.Core.Entities.Identity;
using Amazon.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.ReviewServices.Dto
{
	public class ReviewDto
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

	