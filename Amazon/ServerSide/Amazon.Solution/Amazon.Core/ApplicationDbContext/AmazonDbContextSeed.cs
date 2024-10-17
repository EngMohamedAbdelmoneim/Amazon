using Amazon.Core.DBContext;
using Amazon.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Amazon.Core.ApplicationDbContext
{
	public class AmazonDbContextSeed
	{
		public async static Task SeedAsync(AmazonDbContext _dbContext)
		{
			if (_dbContext.DeliveryMethods.Count() == 0)
			{

				var deliveryMethodsData = File.ReadAllText("../Amazon.Core/DataSeed/delivery.json");
				var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);


				if (deliveryMethods?.Count() > 0)
				{
					foreach (var deliveryMethod in deliveryMethods)
					{
						_dbContext.Set<DeliveryMethod>().Add(deliveryMethod);
					}
					await _dbContext.SaveChangesAsync();
				}
			}
			if (_dbContext.PaymentMethods.Count() == 0)
			{

				var paymentMethodsData = File.ReadAllText("../Amazon.Core/DataSeed/payment.json");
				var paymentMethods = JsonSerializer.Deserialize<List<PaymentMethod>>(paymentMethodsData);


				if (paymentMethods?.Count() > 0)
				{
					foreach (var paymentMethod in paymentMethods)
					{
						_dbContext.Set<PaymentMethod>().Add(paymentMethod);
					}
					await _dbContext.SaveChangesAsync();
				}
			}

		}
	}
}
