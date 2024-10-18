using Amazon.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Core.Entities
{
	public class Review : BaseEntity
	{
		[Range(1, 5)]
		public int Rating { get; set; }
		[MaxLength(100)]
		public string ReviewHeadLine { get; set; }
		[MaxLength(1000)]
		public string ReviewText { get; set; }
		public DateTime ReviewDate { get; set; } = DateTime.Now;
		public int ProductId { get; set; }
		public virtual Product Product { get; set; }
		[ForeignKey("AppUser")]
		public string AppUserName { get; set; }
		public string AppUserEmail { get; set; }
	}
}

