using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WepWarehouse.Data
{
    [Table("WareHouseItems")]
    public class WareHouseItem
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string kUCode { get; set; }

        [Range(1, int.MaxValue)]

        public int QTY { get; set; }
        
        public decimal CostPrice { get; set; }

        public decimal MSRPPrice { get; set; }
        [ForeignKey("wareHouse")]
        public int Warehouse_id { get; set; }


        public WareHouse wareHouse { get; set; }
    }
}
