using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using WepWarehouse.Data;
using WepWarehouse.Models;

namespace WepWarehouse.Services
{
    public class AccountServices : IAccountServices
    {
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;
        RoleManager<IdentityRole> roleManager;
        public AccountServices(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager, RoleManager<IdentityRole> _roleManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }

        public async Task<IdentityResult> CreateAccount(SignUpDTO User)
        {
            ApplicationUser newUser = new ApplicationUser();
            newUser.UserName = User.Email;
            newUser.Email = User.Email;
            newUser.Name = User.Name;
            newUser.DOB = User.DOB;
            // newUser.PasswordHash = User.Password;
            var result = await userManager.CreateAsync(newUser, User.Password);
            if (result.Succeeded)
            {
                var roleResult = await userManager.AddToRoleAsync(newUser, User.RoleName);
                {
                    if (!roleResult.Succeeded)
                    {
                        await userManager.DeleteAsync(newUser);
                    }
                }
            }
            return result;

        }

        public async Task<SignInResult> SigIn(SignInModel signInModel)
        {
            var result = await signInManager.PasswordSignInAsync(signInModel.Username, signInModel.Password, false, false);
            return result;
        }

        public async Task<IdentityResult> AddRole(RoleModel roleModel)
        {
            IdentityRole role = new IdentityRole();
            role.Name = roleModel.Name;
            var result = await roleManager.CreateAsync(role);
            return result;
        }

        public async Task<List<RoleModel>> GetRoles()
        {
            List<IdentityRole> allRoles = await roleManager.Roles.ToListAsync();
            List<RoleModel> roles = new List<RoleModel>();
            foreach (IdentityRole item in allRoles)
            {
                RoleModel role = new RoleModel();
                role.Id = item.Id;
                role.Name = item.Name;
                roles.Add(role);

            }
            return roles;
        }


        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }


        public async Task<List<SignUpDTO>> GetUsers()
        {
            List<ApplicationUser> allUsers = await userManager.Users.ToListAsync();
            List<SignUpDTO> users = new List<SignUpDTO>();

            foreach (ApplicationUser item in allUsers)
            {
                SignUpDTO user = new SignUpDTO();
                user.Id = item.Id;
                user.Name = item.Name;
                user.Email = item.Email;
                user.DOB = item.DOB;

                var roles = await userManager.GetRolesAsync(item);
                user.RoleName = string.Join(", ", roles);

                users.Add(user);
            }

            return users;
        }



        public async Task<IdentityResult> Delete(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "RoleNotFound",
                    Description = $"Role with id {id} not found."
                });
            }

            return await roleManager.DeleteAsync(role);
        }






        public async Task<IdentityResult> DeleteUser(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = $"User with ID {userId} does not exist."
                });
            }

            var roles = await userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                await userManager.RemoveFromRolesAsync(user, roles);
            }

            var result = await userManager.DeleteAsync(user);
            return result;
        }



        public async Task<SignUpDTO> GetCurrentUserInfo(ClaimsPrincipal userPrincipal)
        {
            var user = await userManager.GetUserAsync(userPrincipal);
            if (user == null) return null;

            var roles = await userManager.GetRolesAsync(user);

            return new SignUpDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                DOB = user.DOB,
                RoleName = string.Join(", ", roles)
            };
        }

        public async Task<RoleModel?> GetRoleByIdAsync(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null) return null;

            return new RoleModel
            {
                Id = role.Id,
                Name = role.Name
            };
        }



        public async Task<List<RoleModel>> GetAllRolesAsync()
        {
            return await roleManager.Roles
                .Select(r => new RoleModel
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task<bool> UpdateRoleAsync(RoleModel model)
        {
            Console.WriteLine($"Incoming model: Id={model.Id}, Name={model.Name}");

            var role = await roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                Console.WriteLine($"Role with ID {model.Id} not found.");
                return false;
            }

            Console.WriteLine($"Current DB Role: Id={role.Id}, Name={role.Name}");

            role.Name = model.Name;
            role.NormalizedName = model.Name.ToUpperInvariant();

            var result = await roleManager.UpdateAsync(role);

            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    Console.WriteLine($"Update failed: {err.Description}");
                }
            }
            else
            {
                Console.WriteLine($"Role updated successfully. New Name = {role.Name}");
            }

            return result.Succeeded;


        }
    }
    }
