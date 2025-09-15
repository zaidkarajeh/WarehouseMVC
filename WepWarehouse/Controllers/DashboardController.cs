using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WepWarehouse.Data;
using WepWarehouse.Models;

namespace WepWarehouse.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        WarehouseContext context;
        RoleManager<IdentityRole> roleManager;
        UserManager<ApplicationUser> userManager;
        public DashboardController(WarehouseContext _context, RoleManager<IdentityRole> _roleManager,
                               UserManager<ApplicationUser> _userManager)
        {
            context = _context;
            roleManager = _roleManager;
            userManager = _userManager;
        }


       

        public IActionResult Index()
        {
           
            var vm = new VMDashbord
            {
                WareHouse = context.wareHouses.Count(),
                WareHouseItem = context.wareItems.Count(),
                Country = context.countries.Count(),
                TotalRoles = roleManager.Roles.Count(),     
                TotalUsers = userManager.Users.Count()     


            };

            return View(vm);
        }
    }
}
