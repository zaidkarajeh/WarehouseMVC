using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WepWarehouse.Data;
using WepWarehouse.Models;


namespace WepWarehouse.Services
{
    public class WareHouseService : IWareHouseService
    {
        IMapper mapper;
        WarehouseContext context;
        public WareHouseService(WarehouseContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public void Insert(WareHouseDTO wareHouseDTO)
        {
            WareHouse NEWwareHouse = mapper.Map<WareHouse>(wareHouseDTO);
            context.wareHouses.Add(NEWwareHouse);
            context.SaveChanges();
        }
        public List<WareHouseDTO> GetAllWareHouses()
        {

            List<WareHouse> allWareHouse = context.wareHouses.ToList();
            List<WareHouseDTO> WareHouseGet = mapper.Map<List<WareHouseDTO>>(allWareHouse);
            return WareHouseGet;
        }
        public List<WareHouseDTO> GetWareHouseName(string name)
        {
            List<WareHouse> allWareHouse = context.wareHouses.Include("Country").Where(w => w.Name.Contains(name)).ToList();
            List<WareHouseDTO> wareHouseGet = new List<WareHouseDTO>();
            wareHouseGet = mapper.Map<List<WareHouseDTO>>(allWareHouse);
            return wareHouseGet;
        }
        public void Delete(int id)
        {
            WareHouse wareHouseDelete = context.wareHouses.Where(w => w.Id == id).FirstOrDefault();
            context.wareHouses.Remove(wareHouseDelete);
            context.SaveChanges();
        }
        public WareHouseDTO GetWareHouse(int WareHouseId)
        {
            WareHouse wareHouseGetInf = context.wareHouses.Where(w => w.Id == WareHouseId).FirstOrDefault();
            WareHouseDTO wareHousedtoGet = mapper.Map<WareHouseDTO>(wareHouseGetInf);
            return wareHousedtoGet;
        }

       public void Update( WareHouseDTO dto )
        {
            WareHouse wareHouseUpd = mapper.Map<WareHouse>(dto);
            context.wareHouses.Attach(wareHouseUpd);
            context.Entry(wareHouseUpd).State = EntityState.Modified;
            context.SaveChanges();
        }
       

    }
}
