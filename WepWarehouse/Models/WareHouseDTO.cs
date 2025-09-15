using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WepWarehouse.Data;

namespace WepWarehouse.Models
{
    [AutoMap(typeof(WareHouse), ReverseMap = true)]
    public class WareHouseDTO
    {
        public int Id { get; set; }
       
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]

        public int Country_Id { get; set; }


        public CountryDTO Country { get; set; }



    }
}
