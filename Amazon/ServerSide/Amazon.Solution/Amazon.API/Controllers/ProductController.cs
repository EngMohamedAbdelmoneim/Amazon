using Amazon.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Amazon.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        List<Product> products = new List<Product>();

        Product p1 = new Product() 
        { 
            Id = 1,
            Name = "Samsung Galaxy S24 Ultra",
            PictureUrl = "https://images.samsung.com/is/image/samsung/p6pim/eg/2401/gallery/eg-galaxy-s24-s928-sm-s928bztcmea-thumb-539296180",
            Price = 55000
        };

        Product p2 = new Product()
        {
            Id = 2,
            Name = "Iphone 16",
            PictureUrl = "https://d61s2hjse0ytn.cloudfront.net/color/1050/iphone_16_Pink.webp",
            Price = 100_000
        };

        public ProductController()
        {
            products.Add(p1);
            products.Add(p2);
        }

        
        
        

        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            return products;
        }

        [HttpGet]
        [Route("/Id")]
        public ActionResult<Product> GetProduct(int Id)
        {
            return products.SingleOrDefault(p => p.Id == Id);
        }
    }
}
