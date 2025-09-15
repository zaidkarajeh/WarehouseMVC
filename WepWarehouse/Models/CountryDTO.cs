using AutoMapper;
using System.ComponentModel.DataAnnotations;
using DataCountry = WepWarehouse.Data.Country;

using WepWarehouse.Data;

namespace WepWarehouse.Models
{
    [AutoMap(typeof(Country), ReverseMap = true)]
    public class CountryDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
