using Amazon.Core.DBContext;
using Amazon.Core.Entities;
using Amazon.Core.Entities.OrderAggregate;
using Amazon.Core.IdentityDb;
using Amazon.Services.OrderService;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdminWebApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly AmazonDbContext context;
        private readonly AppIdentityDbContext appIdentityDbContext;

        public OrderController(IOrderService orderService, AmazonDbContext context, AppIdentityDbContext appIdentityDbContext)
        {
            this.orderService = orderService;
            this.context = context;
            this.appIdentityDbContext = appIdentityDbContext;
        }

        public IActionResult Index()
        {
            var orders = context.Orders.ToList();
            return View(orders);
        }

        public IActionResult Create()
        {
            var addresses = appIdentityDbContext.Addresses.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ParentCategoryId,Id")] Order order)
        {

            return View(order);
        }
    }
}
