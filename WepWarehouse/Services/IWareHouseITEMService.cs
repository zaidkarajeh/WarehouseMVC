using WepWarehouse.Models;

namespace WepWarehouse.Services
{
    public interface IWareHouseITEMService
    {
        void Insert(WareHouseItemDTO wareHouseItemDTO);

        List<WareHouseItemDTO> GetWarehouseItemByName(string Name);

        void Delete(int id);

        WareHouseItemDTO Getitem(int itemId);


        void Update(WareHouseItemDTO dto);

    }
}