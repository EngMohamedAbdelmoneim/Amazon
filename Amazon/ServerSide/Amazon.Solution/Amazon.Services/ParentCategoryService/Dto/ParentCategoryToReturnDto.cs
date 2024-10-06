using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Core.Entities;
using Amazon.Services.CategoryServices.Dto;
namespace Amazon.Services.ParentCategoryService.Dto

{
    public class ParentCategoryToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> Categories { get; set; } =[];

    }
}
