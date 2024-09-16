using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Core.Entities
{
	public class ParentCategory : BaseEntity
	{
        public string Name { get; set; }

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
