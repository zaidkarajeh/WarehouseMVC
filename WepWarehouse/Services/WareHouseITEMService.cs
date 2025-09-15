using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WepWarehouse.Data;
using WepWarehouse.Models;

namespace WepWarehouse.Services
{
    public class WareHouseITEMService : IWareHouseITEMService
    {
        WarehouseContext context;
        IMapper mapper;
        public WareHouseITEMService(WarehouseContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public void Insert(WareHouseItemDTO wareHouseItemDTO)
        {
            WareHouseItem NEWwareHouseItem = mapper.Map<WareHouseItem>(wareHouseItemDTO);
            context.wareItems.Add(NEWwareHouseItem);
            context.SaveChanges();
        }

        public List<WareHouseItemDTO> GetWarehouseItemByName( string warehouseName)
        {
            List<WareHouseItem> allwareHouseItems = context.wareItems.Include("wareHouse").Where(w => w.wareHouse.Name == warehouseName).ToList();
            List<WareHouseItemDTO> itemGET = new List<WareHouseItemDTO>();
            itemGET = mapper.Map<List<WareHouseItemDTO>>(allwareHouseItems);
            return itemGET;

        }

        public void Delete( int id)
        {
            WareHouseItem wareHouseItemDelete = context.wareItems.Where(w => w.Id == id).FirstOrDefault();
            context.wareItems.Remove(wareHouseItemDelete);
            context.SaveChanges();
        }


         public WareHouseItemDTO Getitem(int itemId)
         {
           WareHouseItem wareHouseItemgGETInfo = context.wareItems.Where(w=> w.Id == itemId).FirstOrDefault();
           WareHouseItemDTO wareHouseItemDTOGet = mapper.Map<WareHouseItemDTO>(wareHouseItemgGETInfo);
          return wareHouseItemDTOGet;
         }

        public void Update( WareHouseItemDTO dto)
        {
            WareHouseItem wareHouseItemUp = mapper.Map<WareHouseItem>(dto);
            context.wareItems.Attach(wareHouseItemUp);
            context.Entry(wareHouseItemUp).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
