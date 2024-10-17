namespace Amazon.Core.Entities.OrderAggregate
{
	public class ShippingAddress
	{

		public ShippingAddress()
		{

		}
		public ShippingAddress(string firstName, string lastName, string phoneNumber, string country, string governorate, string city, string district, string streetAddress, string buildingName, string nearestLandMark)
		{
			FirstName = firstName;
			LastName = lastName;
			PhoneNumber = phoneNumber;
			Country = country;
			Governorate = governorate;
			City = city;
			District = district;
			StreetAddress = streetAddress;
			BuildingName = buildingName;
			NearestLandMark = nearestLandMark;
		}

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public string Country { get; set; }
		public string Governorate { get; set; }
		public string City { get; set; }
		public string District { get; set; }
		public string StreetAddress { get; set; }
		public string BuildingName { get; set; }
		public string NearestLandMark { get; set; }
	}
}
