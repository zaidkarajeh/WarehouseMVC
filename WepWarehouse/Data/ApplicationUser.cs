using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using WepWarehouse.Models;

namespace WepWarehouse.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public DateTime DOB { get; set; }

       


    }
}
