using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using WepWarehouse.Models;

namespace WepWarehouse.Services
{
    public interface IAccountServices
    {

        Task<IdentityResult> CreateAccount(SignUpDTO User);

        Task<SignInResult> SigIn(SignInModel signInModel);

        Task<IdentityResult> AddRole(RoleModel roleModel);

        Task<List<RoleModel>> GetRoles();

        Task Logout();
        Task<List<SignUpDTO>> GetUsers();

        Task<IdentityResult> Delete(string id);


        Task<IdentityResult> DeleteUser(string userId);

        Task<SignUpDTO> GetCurrentUserInfo(ClaimsPrincipal userPrincipal);

        Task<List<RoleModel>> GetAllRolesAsync();

        
    }
}