using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WepWarehouse.Data;

namespace WepWarehouse.Models
{
    [AutoMap(typeof(WareHouseItem), ReverseMap = true)]

    public class WareHouseItemDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string kUCode { get; set; }

        [Range(1, int.MaxValue)]
        [Required]


        public int QTY { get; set; }

        public decimal CostPrice { get; set; }
        [Required]


        public decimal MSRPPrice { get; set; }
        [Required]

        public int Warehouse_id { get; set; }


        public WareHouseDTO wareHouse { get; set; }
    }
}
