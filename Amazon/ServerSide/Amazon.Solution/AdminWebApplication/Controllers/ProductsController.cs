using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Amazon.Core.DBContext;
using Amazon.Core.Entities;
using Amazon.Services.ProductService;
using Amazon.Services.ProductService.Dto;
using Amazon.Services.BrandService;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace AdminWebApplication.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AmazonDbContext _context;
        private readonly IProductService productService;
        private readonly IBrandService brandService;

        public ProductsController(AmazonDbContext context, IProductService productService, IBrandService brandService)
        {
            _context = context;
            this.productService = productService;
            this.brandService = brandService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllProductsAsync();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            var brands = _context.Brands.ToList();
            var categories = _context.Categories.ToList();
            ViewBag.Brands = new SelectList(brands, "Id", "Name");
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile productImg, ProductToReturnDto product)
        {

            if(productImg != null)
            {

                var otherProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "../Amazon.API/wwwroot/Files/productImages");

                //string FileExtension = productImg.FileName.Split('.').Last();
                //string FilePath = $"~/Amazon.API/Files/productImages/{productImg.FileName}";
                //Console.BackgroundColor = ConsoleColor.Green;
                //Console.WriteLine(FilePath);
                Console.WriteLine(otherProjectPath);
                //Console.ResetColor();

                using (FileStream st = new FileStream(otherProjectPath, FileMode.Create))
                {
                    await productImg.CopyToAsync(st);
                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.WriteLine("Null");
                Console.ResetColor();
            }


            //if (ModelState.IsValid)
            //{
            //    _context.Add(product);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", product.BrandId);
            //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var brands = await brandService.GetAllBrandsAsync();
            ViewData["BrandId"] = new SelectList(brands, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Price,PictureUrl,QuantityInStock,CategoryId,BrandId,Id")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brands, "Id", "Id", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
