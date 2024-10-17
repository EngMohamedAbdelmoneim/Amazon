namespace Amazon.Core.Entities.OrderAggregate
{
	public class ProductItemOrdered
	{
        public ProductItemOrdered()
        {
            
        }

		public ProductItemOrdered(int productId, string productName, string pictureUrl, string category, string brand)
		{
			ProductId = productId;
			ProductName = productName;
			PictureUrl = pictureUrl;
			Category = category;
			Brand = brand;
		}

		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public string PictureUrl { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
    }
}
