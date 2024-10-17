using Amazon.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Infrastructure.Specification.OrderSpecefifcations
{
	public class OrderSpecifications :BaseSpecification<Order>
	{
        public OrderSpecifications(string buyerEmail) 
            :base(o =>o.BuyerEmail == buyerEmail) 
        {
            AddIncludes();

            AddOrderByDesc(o => o.OrderDate);
        }
        
        public OrderSpecifications(int orderId,string buyerEmail) 
            :base(o =>o.BuyerEmail == buyerEmail && o.Id == orderId  ) 
        {
            AddIncludes();
        }

		private void AddIncludes()
		{
			Includes.Add(o => o.DeliveryMethod);
			Includes.Add(o => o.PaymentMethod);
			Includes.Add(o => o.Items);
		}
	}
}
