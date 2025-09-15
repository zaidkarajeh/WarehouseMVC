using WepWarehouse.Models;

namespace WepWarehouse.Services
{
    public interface IWareHouseService
    {
        void Insert(WareHouseDTO wareHouseDTO);

        List<WareHouseDTO> GetWareHouseName(string name);
        void Delete(int id);

        WareHouseDTO GetWareHouse(int WareHouseId);

        void Update(WareHouseDTO dto);
        List<WareHouseDTO> GetAllWareHouses();
    }
}