using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Core.Entities;
namespace Amazon.Services.ParentCategoryService.Dto

{
    public class ParentCategoryToReturnDto
    {
        public string Name { get; set; }

        public ICollection<Category> Categories { get; set; } = new List<Category>();

    }
}
