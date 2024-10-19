using Amazon.Core.Entities;
using Amazon.Core.Entities.OrderAggregate;
using Amazon.Services.CartService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Services.PaymentService
{
    public interface IPaymentService
    {
        Task<CartDto> SetPaymentIntent(string cartId);
        Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId);
        Task<Order> UpdateOrderPaymentFailed(string paymentIntentId);
    }
}
