using Amazon.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.ParentCategoryService.Dto
{
    public class ParentCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
