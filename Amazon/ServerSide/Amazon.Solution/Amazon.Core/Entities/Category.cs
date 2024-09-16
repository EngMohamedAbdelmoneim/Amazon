namespace Amazon.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int ParentCategoryId { get; set; }
        public virtual ParentCategory ParentCategory { get; set; }
    }
}
