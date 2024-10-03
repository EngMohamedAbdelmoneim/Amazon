namespace Amazon.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        //public string? Description { get; set; }
        //public string? Type { get; set; }
        //public int? DisplayOrder {  get; set; }
        //public bool? IsActive { get; set; }
        //public bool? IsVisible { get; set; }
        public int ParentCategoryId { get; set; }
        public virtual ParentCategory ParentCategory { get; set; }
    }
}
