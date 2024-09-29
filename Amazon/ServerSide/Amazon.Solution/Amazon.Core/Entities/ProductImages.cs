namespace Amazon.Core.Entities
{
	public class ProductImages : BaseEntity
	{
        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
        public string ImagePath { get; set; }
    }
}
