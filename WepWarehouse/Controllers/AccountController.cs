using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WepWarehouse.Data;
using WepWarehouse.Models;
using WepWarehouse.Services;

namespace WepWarehouse.Controllers
{
    // [Authorize(Roles = "Manager")]
    public class AccountController : Controller
    {
        IAccountServices accountServices;
        RoleManager<IdentityRole> roleManager;
        UserManager<ApplicationUser> userManager;
        public AccountController(IAccountServices _accountServices, RoleManager<IdentityRole> _roleManager, UserManager<ApplicationUser> _userManager)
        {
            accountServices = _accountServices;
            roleManager = _roleManager;
            userManager = _userManager;
        }
        public async Task<IActionResult> SignUp()
        {
            VMSignUp vM = new VMSignUp();
            vM.VMRolesDTO = await accountServices.GetRoles();
            return View("SignUp", vM);
        }

        public async Task<IActionResult> CreateAccount(VMSignUp vM)
        {
            var result = await accountServices.CreateAccount(vM.VMsignUpDTO);
            vM.VMRolesDTO = await accountServices.GetRoles();

            return View("SignUp", vM);
        }

        public IActionResult SignIn()
        {
            ViewData["IsEdit"] = false;
            ViewData["InValid"] = false;

            return View("SignIn");
        }


        public async Task<IActionResult> Login(SignInModel model)
        {
            var result = await accountServices.SigIn(model);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewData["InValid"] = true;

                return View("SignIn");
            }

        }

        public IActionResult AddRole()
        {
            ViewData["InAdded"] = false;
            ViewData["IsEdit"] = false;

            return View("NewRole");
        }

        public async Task<IActionResult> SaveRole(RoleModel role)
        {
            ViewData["IsEdit"] = false;
            ViewData["InAdded"] = true;

            var result = await accountServices.AddRole(role);
            return View("NewRole");
        }
        public async Task<IActionResult> Rolelist()
        {
            var roles = await accountServices.GetAllRolesAsync();
            return View("RoleList", roles);
        }
        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }



        public async Task<IActionResult> Logout()
        {
            await accountServices.Logout();
            return RedirectToAction("SignIn");
        }

        public async Task<IActionResult> UsersList()

        {

            List<SignUpDTO> signUpDTOs = await accountServices.GetUsers();
            return View("UsersList", signUpDTOs);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await accountServices.Delete(id);
            List<RoleModel> roleModels = await accountServices.GetRoles();

            return View("Rolelist", roleModels);
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await accountServices.DeleteUser(id); // call the service method

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            var users = await accountServices.GetUsers();
            return View("UsersList", users);
        }



        public async Task<IActionResult> Profile()
        {
            var userInfo = await accountServices.GetCurrentUserInfo(User);
            if (userInfo == null)
                return RedirectToAction("SignIn"); 

            return View("Profile", userInfo);
        }





    }

}
