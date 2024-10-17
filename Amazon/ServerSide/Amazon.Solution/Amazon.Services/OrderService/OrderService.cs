using Amazon.Core.Entities;
using Amazon.Core.Entities.Identity;
using Amazon.Core.Entities.OrderAggregate;
using Amazon.Services.OrderService.OrderDto;
using Amazone.Infrastructure.Interfaces;
using Amazone.Infrastructure.Specification.OrderSpecefifcations;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Amazon.Services.OrderService
{
	public class OrderService : IOrderService
	{
		private readonly IGenericCacheRepository<Cart> _cartRepo;
		private readonly IGenericRepository<Product> _productRepo;
		private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
		private readonly IGenericRepository<PaymentMethod> _paymentMethodRepo;
		private readonly IGenericRepository<Order> _orderRepo;
		private readonly UserManager<AppUser> _userManager;
		private readonly IMapper _mapper;

		public OrderService(IGenericCacheRepository<Cart> cartRepo,
			IGenericRepository<Product> productRepo,
			IGenericRepository<DeliveryMethod> deliveryMethodRepo,
			IGenericRepository<PaymentMethod> paymentMethodRepo,
			IGenericRepository<Order> orderRepo,
			UserManager<AppUser> userManager,
			IMapper mapper)
        {
			_cartRepo = cartRepo;
			_productRepo = productRepo;
			_deliveryMethodRepo = deliveryMethodRepo;
			_paymentMethodRepo = paymentMethodRepo;
			_orderRepo = orderRepo;
			_userManager = userManager;
			_mapper = mapper;
		}
        public async Task<OrderToReturnDto> CreateOrderAsync(string buyerEmail, string cartId,int paymentMethodId ,int deliveryMethodId, string shippingAddressId)
		{
			var cart = await _cartRepo.GetAsync(cartId);
			if (cart == null)
				return null;

			var user =await _userManager.Users.Include(u => u.Addresses).SingleOrDefaultAsync(u => u.Email == buyerEmail);

			var userAddresses = user.Addresses.ToList();
			var Address = user.Addresses.FirstOrDefault(x => x.Id == shippingAddressId);
			if (Address == null)
				return null;

			var shippingAddress = _mapper.Map<ShippingAddress>(Address);

			var orderItems = new List<OrderItem>();

			if (cart?.Items?.Count > 0)
			{
				foreach (var item in cart.Items)
				{
					var product = await _productRepo.GetByIdAsync(item.Id);

					if (product.QuantityInStock < item.Quantity)
						return null;

					product.QuantityInStock -= item.Quantity;
					await _productRepo.Update(product);
					
					var productItemOrder = new ProductItemOrdered(item.Id, product.Name,product.PictureUrl,product.Category.Name,product.Brand.Name);
					var orderItem = new OrderItem(productItemOrder, product.Price, item.Quantity);

					orderItems.Add(orderItem);
				}
			}

			var subtotal = orderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity);
			var deliveryMethod = await _deliveryMethodRepo.GetByIdAsync(deliveryMethodId);
			var paymentMethod = await _paymentMethodRepo.GetByIdAsync(paymentMethodId);

			if (paymentMethod.Id == 1)
			{
				var order = new Order(buyerEmail,shippingAddressId,shippingAddress ,paymentMethod,deliveryMethod,orderItems,subtotal);
				var result = await _orderRepo.Add(order);

				if (result <= 0)
					return null;
				return _mapper.Map<OrderToReturnDto>(order);
			}
			else
				return null;
		}

		public async Task<OrderToReturnDto> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
		{
			var spec = new OrderSpecifications(orderId, buyerEmail);
			var order = await _orderRepo.GetWithSpecAsync(spec);

			var user = await _userManager.Users.Include(u => u.Addresses).SingleOrDefaultAsync(u => u.Email == buyerEmail);

			var address = user.Addresses.FirstOrDefault(address => address.Id == order.ShippingAddressId);
			var shippingAddress = _mapper.Map<ShippingAddress>(address);
			order.ShippingAddress = shippingAddress;

			if (order == null) 
				return null;	

			return _mapper.Map<OrderToReturnDto>(order);
		}

		public async Task<IReadOnlyList<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail)
		{

			var spec = new OrderSpecifications(buyerEmail);

			var user = await _userManager.Users.Include(u => u.Addresses).SingleOrDefaultAsync(u => u.Email == buyerEmail);

			var orders = await _orderRepo.GetAllWithSpecAsync(spec);
			foreach (var order in orders)
			{
				var address = user.Addresses.FirstOrDefault(address => address.Id == order.ShippingAddressId);
				var orderAddress = _mapper.Map<ShippingAddress>(address);
				order.ShippingAddress = orderAddress;
			}
			
			if (orders == null) 
				return null;

			return _mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders);
		}
	}
}
