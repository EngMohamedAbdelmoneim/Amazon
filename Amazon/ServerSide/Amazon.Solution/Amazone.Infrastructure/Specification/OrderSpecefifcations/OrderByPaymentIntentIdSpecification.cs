using Amazon.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazone.Infrastructure.Specification.OrderSpecefifcations
{
    public class OrderByPaymentIntentIdSpecification : BaseSpecification<Order>
    {
        public OrderByPaymentIntentIdSpecification(string paymentIntentId)
            : base(o => o.PaymentIntetId == paymentIntentId)
        {
        }
    }
}
