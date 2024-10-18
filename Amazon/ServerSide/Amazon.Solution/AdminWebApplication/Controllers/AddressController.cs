using Amazon.Core.Entities;
using Amazon.Core.Entities.Identity;
using Amazon.Core.IdentityDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AdminWebApplication.Controllers
{
    public class AddressController : Controller
    {
        private readonly AppIdentityDbContext appIdentityDbContext;

        public AddressController(AppIdentityDbContext appIdentityDbContext)
        {
            this.appIdentityDbContext = appIdentityDbContext;
        }


        public IActionResult Index()
        {
            var addresses = appIdentityDbContext.Addresses.ToList();
            return View(addresses);
        }

        public IActionResult Create()
        {
            var users = appIdentityDbContext.Users.ToList();
            Console.WriteLine(users[0].DisplayName);
            ViewBag.Users = new SelectList(users, "Id", "DisplayName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(
            "FirstName,LastName,PhoneNumber, Country," +
            " StreetAddress, BuildingName, City, District," +
            " Governorate, NearestLandMark, AppUserId")] Address address)
        {
            if (ModelState.IsValid)
            {
                Guid id = Guid.NewGuid();
                address.Id = id.ToString();
                appIdentityDbContext.Addresses.Add(address);
                await appIdentityDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }
    }
}
