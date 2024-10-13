namespace Amazon.Services.Utilities.EmailSettings
{
	public class Email
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Body { get; set; }
		public string To { get; set; }
		public bool IsHtml { get; set; } = false;
	}
}
