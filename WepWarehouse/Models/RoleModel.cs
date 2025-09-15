using System.ComponentModel.DataAnnotations;
using WepWarehouse.Data;

namespace WepWarehouse.Models
{
    public class RoleModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string RoleName { get; set; }

        public List<SignUpDTO> SignUpDTO { get; set; }
    }
}
