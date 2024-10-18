namespace Amazon.Core.Entities
{
	public class ParentCategory : BaseEntity
	{
        public string Name { get; set; }

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    }
}
