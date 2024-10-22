using Amazon.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminWebApplication.Controllers
{
    public class SellerController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public SellerController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var sellers = await _userManager.GetUsersInRoleAsync("Seller");
            return View(sellers);
        }

        // GET: Sellers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();
            var seller = await _userManager.FindByIdAsync(id);
            if (seller == null) return NotFound();
            return View(seller);
        }

        // POST: Sellers/Delete/5
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
    }
}
