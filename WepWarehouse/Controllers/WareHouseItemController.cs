using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WepWarehouse.Data;
using WepWarehouse.Models;
using WepWarehouse.Services;

namespace WepWarehouse.Controllers
{
    [Authorize(Roles = "Employee")]
    public class WareHouseItemController : Controller
    {
        IWareHouseService wareHouseService;
        IWareHouseITEMService wareHouseITEMService;
        public WareHouseItemController(IWareHouseService _wareHouseService, IWareHouseITEMService _wareHouseITEMService)
        {
            wareHouseService = _wareHouseService;
            wareHouseITEMService = _wareHouseITEMService;
        }
        public IActionResult Index()

        {
            ViewData["InUpdata"] = false;
            ViewData["InValid"] = false;
            ViewData["IsEdit"] = false;
            VMitem vm = new VMitem();
            vm.VMwareHouseDTO = new List<WareHouseDTO>();
            vm.VMwareHouseDTO = wareHouseService.GetAllWareHouses();
            return View("NEWwareHouseItem", vm);
        }

        public IActionResult Save(VMitem vm)
        {
            ViewData["InUpdata"] = false;
            ViewData["InValid"] = true;
            ViewData["IsEdit"] = false;
            wareHouseITEMService.Insert(vm.VMItemDTO);
            vm.VMwareHouseDTO = new List<WareHouseDTO>();
            vm.VMwareHouseDTO = wareHouseService.GetAllWareHouses();
            return View("NEWwareHouseItem", vm);

        }

        public IActionResult ItemList()
        {
            List<WareHouseItemDTO> itemlist = new List<WareHouseItemDTO>();
            return View("ItemList", itemlist);
        }

        public IActionResult Searchwarehouse()
        {
            string name = Request.Form["txtSearchName"];
            List<WareHouseItemDTO> wareHouseslSea = wareHouseITEMService.GetWarehouseItemByName(name);
            return View("ItemList", wareHouseslSea);
        }
        public IActionResult Delete(int WareitemID)
        {
            wareHouseITEMService.Delete(WareitemID);
            List<WareHouseItemDTO> wareHouseItemLi = new List<WareHouseItemDTO>();
            return View("ItemList", wareHouseItemLi);
        }

        public IActionResult Edit(int ItemID)
        {
            ViewData["InValid"] = false;
            ViewData["InUpdata"] = false;

            ViewData["IsEdit"] = true;
            VMitem vm = new VMitem();
            WareHouseItemDTO wareHouseItemEdit = wareHouseITEMService.Getitem(ItemID);
            vm.VMItemDTO = wareHouseItemEdit;
            vm.VMwareHouseDTO = wareHouseService.GetAllWareHouses();
            return View("NEWwareHouseItem", vm);
        }

        public IActionResult Update(VMitem VM)
        {
            ViewData["InValid"] = false;
            ViewData["InUpdata"] = true;

            wareHouseITEMService.Update(VM.VMItemDTO);
            ViewData["IsEdit"] = true;
            VM.VMwareHouseDTO = wareHouseService.GetAllWareHouses();
            return View("NEWwareHouseItem", VM);

        }


    }
}
