using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WepWarehouse.Data;
using WepWarehouse.Models;
using WepWarehouse.Services;

namespace WepWarehouse.Controllers
{
    [Authorize(Roles = "Manager")]
    public class WareHouseController : Controller

    {
        ICountryService countryService;
        IWareHouseService wareHouseService;
        public WareHouseController(ICountryService _countryService, IWareHouseService _wearHouseService)
        {
            countryService = _countryService;
            wareHouseService = _wearHouseService;
        }
        public IActionResult Index()
        {
            ViewData["InUpdata"] = false;
            ViewData["InValid"] = false;
            ViewData["IsEdit"] = false;
            VMWareHouse vM = new VMWareHouse();
            vM.VMcountryDTO = new List<CountryDTO>();
            vM.VMcountryDTO= countryService.GetAllCountries();
            return View("NewWareHouse", vM );
        }

        public IActionResult Save( VMWareHouse vM) 
        {
            ViewData["InUpdata"] = false;
            ViewData["InValid"] = true;
            ViewData["IsEdit"] = false;
            wareHouseService.Insert(vM.VMwareHouseDTO);
            vM.VMcountryDTO = new List<CountryDTO>();
            vM.VMcountryDTO = countryService.GetAllCountries();
            return View("NewWareHouse", vM);
        }

        public IActionResult WarehouseList()
        {
            List<WareHouseDTO> wareHouseslist = new List<WareHouseDTO>();

            return View("WarehouseList", wareHouseslist);
        }

       public IActionResult SearchWarehouse()
        {
            string name = Request.Form["txtSearchName"];
            List<WareHouseDTO> wareHouseslSea = wareHouseService.GetWareHouseName(name);
            return View("WarehouseList", wareHouseslSea); 
        }

        public IActionResult Delete(int WareHouseID)
        {
            wareHouseService.Delete(WareHouseID);
            List<WareHouseDTO> wareHouseLi = new List<WareHouseDTO>();
            return View("WarehouseList", wareHouseLi);
        }
        public IActionResult Edit(int WareHouseId)
        {
            ViewData["InValid"] = false;
            ViewData["InUpdata"] = false;
            ViewData["IsEdit"] = true;
            VMWareHouse vM = new VMWareHouse();
            WareHouseDTO wareHouseEdit = wareHouseService.GetWareHouse(WareHouseId);
            vM.VMwareHouseDTO = wareHouseEdit;
            vM.VMcountryDTO = countryService.GetAllCountries();
            return View("NewWareHouse" , vM);
        }
        public IActionResult Updata( VMWareHouse Vm)
        {
            ViewData["InValid"] = false;
            ViewData["InUpdata"] = true;
            wareHouseService.Update(Vm.VMwareHouseDTO);
            ViewData["IsEdit"] = true;
            Vm.VMcountryDTO = countryService.GetAllCountries();
            return View("NewWareHouse", Vm);
        }



    }
}
