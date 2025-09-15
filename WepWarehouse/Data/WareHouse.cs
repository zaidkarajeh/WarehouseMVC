using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WepWarehouse.Data
{
    [Table("Warehouse")]
    public class WareHouse
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Description { get; set; }


        [ForeignKey("Country")]
        public int Country_Id { get; set; }

        public Country Country { get; set; }

       public List<WareHouseItem> wareHouseItems { get; set; }

    }
}
