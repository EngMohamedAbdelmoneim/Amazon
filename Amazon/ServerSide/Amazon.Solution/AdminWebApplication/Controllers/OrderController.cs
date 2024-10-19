using Amazon.Core.DBContext;
using Amazon.Core.Entities;
using Amazon.Core.Entities.OrderAggregate;
using Amazon.Core.IdentityDb;
using Amazon.Services.OrderService;
using Amazon.Services.OrderService.OrderDto;
using Amazone.Infrastructure.Interfaces;
using Amazone.Infrastructure.Repos;
using AutoMapper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Collections;

namespace AdminWebApplication.Controllers
{
    public class OrderController : Controller
    {
        //private readonly IOrderService orderService;
        private readonly IGenericRepository<Order> orderRepo;
        private readonly AmazonDbContext context;
        private readonly AppIdentityDbContext appIdentityDbContext;
        private readonly IMapper mapper;

        public OrderController(AmazonDbContext context, AppIdentityDbContext appIdentityDbContext, IGenericRepository<Order> orderRepo, IMapper mapper)
        {
            //this.orderService = orderService;
            this.context = context;
            this.appIdentityDbContext = appIdentityDbContext;
            this.orderRepo = orderRepo;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await orderRepo.GetAllAsync();
            var mappedOrders = mapper.Map<List<OrderToReturnDto>>(orders);
            return View(mappedOrders);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await orderRepo.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var statusList = new List<Enum> { OrderStatus.Cancelled, OrderStatus.Pending, OrderStatus.Processing, OrderStatus.Shipped, OrderStatus.Delivered };

            ViewBag.Status = new SelectList(statusList);

            var mappedOrder = mapper.Map<OrderToReturnDto>(order);

            return View(mappedOrder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string orderStatus)
        {
            var Order = await context.Orders.FindAsync(id);
            //Order.OrderStatus =;
            if (Order == null)
            {
                return RedirectToAction(nameof(Index));
            }

            switch (orderStatus)
            {
                case "Pending":
                    Order.OrderStatus = OrderStatus.Pending;
                    break;
                case "Cancelled":
                    Order.OrderStatus = OrderStatus.Cancelled;
                    break;
                case "Processing":
                    Order.OrderStatus = OrderStatus.Processing;
                    break;
                case "Shipped":
                    Order.OrderStatus = OrderStatus.Shipped;
                    break;
                case "Delivered":
                    Order.OrderStatus = OrderStatus.Delivered;
                    break;
                default:
                    break;
            }

            context.Entry(Order).State = EntityState.Modified;
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

