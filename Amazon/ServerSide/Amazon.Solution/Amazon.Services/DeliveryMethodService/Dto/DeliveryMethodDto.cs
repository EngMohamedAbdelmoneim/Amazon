namespace Amazon.Services.DeliveryMethodService.Dto
{
	public class DeliveryMethodDto
	{
        public int Id { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
		public decimal Cost { get; set; }
		public string DeliveryTime { get; set; }
	}
}
