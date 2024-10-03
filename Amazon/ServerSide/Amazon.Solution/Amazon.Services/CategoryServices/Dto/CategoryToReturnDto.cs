using Amazon.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.CategoryServices.Dto
{
    public class CategoryToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public string Type { get; set; }
        public string ParentCategoryName { get; set; }
    }
}
