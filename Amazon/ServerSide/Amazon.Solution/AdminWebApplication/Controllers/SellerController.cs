using AdminWebApplication.Models;
using Amazon.Core.DBContext;
using Amazon.Core.Entities.Identity;
using Amazon.Core.IdentityDb;
using Amazon.Core.IdentityDb.migrations;
using Amazon.Services.ProductService.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace AdminWebApplication.Controllers
{
    public class SellerController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppIdentityDbContext _identityContext;
        private readonly AmazonDbContext _context;
        private readonly IMapper _mapper;

        public SellerController(UserManager<AppUser> userManager,
            AppIdentityDbContext identityContext, AmazonDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _identityContext = identityContext;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var sellers = await _userManager.GetUsersInRoleAsync("Seller");
            return View(sellers);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();
            var seller = await _userManager.FindByIdAsync(id);
            if (seller == null) return NotFound();
            return View(seller);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var seller = await _userManager.FindByIdAsync(id);
            if (seller != null)
            {
                await _userManager.DeleteAsync(seller);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();
            var seller = await _userManager.FindByIdAsync(id);
            if (seller == null) return NotFound();
            var sellerProducts = await _context.Products
                .Where(p => p.SellerName == seller.SellerName) 
                .ToListAsync();

            var viewModel = new SellerDetailsViewModel
            {
                Seller = seller,
                Products = _mapper.Map<IEnumerable<ProductToReturnDto>>(sellerProducts)
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleVerification(string id)
        {
            var seller = await _identityContext.Users.FindAsync(id);
            if (seller == null)
            {
                return NotFound("Seller not found.");
            }
            seller.IsActiveSeller = !seller.IsActiveSeller;
            _identityContext.Users.Update(seller);
            await _identityContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Product/ToggleAcceptance/5
        public async Task<IActionResult> ToggleAcceptance(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            return View("ProductDetailsVerification", _mapper.Map<ProductToReturnDto>(product));
        }

        // POST: Product/ToggleAcceptance/5
        [HttpPost, ActionName("ToggleAcceptance")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleAcceptanceConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            product.IsAccepted = !product.IsAccepted;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();


            var sellerId = await _identityContext.Users.FirstOrDefaultAsync(
                u => u.SellerName == product.SellerName);
            return RedirectToAction(nameof(Details), sellerId);
        }
    }
}
