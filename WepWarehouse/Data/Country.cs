using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WepWarehouse.Data
{
    [Table("Countries")]
    public class Country
    {
       
        public int Id { get; set; }
        public string Name { get; set; }

        public List<WareHouse> wareHouses { get; set; }
    }
}
