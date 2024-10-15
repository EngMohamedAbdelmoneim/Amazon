using AdminWebApplication.Models;
using Amazon.Services.BrandService;
using Amazon.Services.ProductService;
using Amazon.Services.ProductService.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdminWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;
        private readonly IBrandService brandService;

        public HomeController(IProductService productService, IBrandService brandService)
        {
            this.productService = productService;
            this.brandService = brandService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllProductsAsync();
            var brands = await brandService.GetAllBrandsAsync();
            var lowQuantityProducts = products.Where(p => p.QuantityInStock < 10).ToList();
            DashboardData data = new() 
            { 
                ProductsNo = products.Count,
                BrandsNo = brands.Count,
                Products = lowQuantityProducts
            };
            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


    public class DashboardData
    {
        public int ProductsNo { get; set; }
        public int UsersNo { get; set; }
        public int BrandsNo { get; set; }
        public List<ProductToReturnDto> Products { get; set; }
    }
}
