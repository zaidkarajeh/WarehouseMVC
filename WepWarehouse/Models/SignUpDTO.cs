using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WepWarehouse.Models
{
    public class SignUpDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "date")]
        public DateTime DOB { get; set; }
        [EmailAddress]
        [Required]

        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string RoleName { get; set; }

        public RoleModel Role { get; set; }


    }
}
