using System.ComponentModel.DataAnnotations;
namespace Amazon.Services.CategoryServices.Dto
{
    public class CategoryDto
    {
        [Required, MaxLength(25)]
        public string Name { get; set; }
        public int ParentCategoryId { get; set; }

        //[Required, MaxLength(100)]
        //public string Description { get; set; }

        //[Required]
        //public string Type { get; set; }

        //[Required]
        //public int DisplayOrder { get; set; }

        //[Required]
        //public bool IsActive { get; set; }

        //[Required]
        //public bool IsVisible { get; set; }

    }
}
