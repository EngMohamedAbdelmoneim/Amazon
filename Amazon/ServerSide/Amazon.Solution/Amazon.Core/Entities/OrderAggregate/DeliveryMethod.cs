namespace Amazon.Core.Entities.OrderAggregate
{
	public class DeliveryMethod : BaseEntity
	{
        public DeliveryMethod()
        {
            
        }

		public DeliveryMethod(string name, string description, decimal cost, string deliveryTime)
		{
			Name = name;
			Description = description;
			Cost = cost;
			DeliveryTime = deliveryTime;
		}

		public string Name { get; set; }
		public string Description { get; set; }
        public decimal Cost { get; set; }
        public string DeliveryTime { get; set; }
    }
}
