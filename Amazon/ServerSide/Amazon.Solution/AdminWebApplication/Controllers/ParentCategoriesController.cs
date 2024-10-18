using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Amazon.Core.DBContext;
using Amazon.Core.Entities;
using Amazon.Services.CategoryServices;
using Amazon.Services.ParentCategoryService;

namespace AdminWebApplication.Controllers
{
    public class ParentCategoriesController : Controller
    {
        private readonly AmazonDbContext _context;
        private readonly IParentCategoryService parentCategoryService;

        public ParentCategoriesController(AmazonDbContext context, IParentCategoryService parentCategoryService)
        {
            _context = context;
            this.parentCategoryService = parentCategoryService;
        }

        // GET: ParentCategories
        public async Task<IActionResult> Index()
        {
            var parentCategories = await parentCategoryService.GetAllParentCategoriesAsync();
            return View(parentCategories);
        }

        // GET: ParentCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parentCategory = await _context.ParentCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parentCategory == null)
            {
                return NotFound();
            }

            return View(parentCategory);
        }

        // GET: ParentCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ParentCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] ParentCategory parentCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parentCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parentCategory);
        }

        // GET: ParentCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parentCategory = await _context.ParentCategories.FindAsync(id);
            if (parentCategory == null)
            {
                return NotFound();
            }
            return View(parentCategory);
        }

        // POST: ParentCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id")] ParentCategory parentCategory)
        {
            if (id != parentCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parentCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParentCategoryExists(parentCategory.Id))
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
            return View(parentCategory);
        }

        // GET: ParentCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parentCategory = await _context.ParentCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parentCategory == null)
            {
                return NotFound();
            }

            return View(parentCategory);
        }

        // POST: ParentCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parentCategory = await _context.ParentCategories.FindAsync(id);
            if (parentCategory != null)
            {
                _context.ParentCategories.Remove(parentCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParentCategoryExists(int id)
        {
            return _context.ParentCategories.Any(e => e.Id == id);
        }
    }
}
