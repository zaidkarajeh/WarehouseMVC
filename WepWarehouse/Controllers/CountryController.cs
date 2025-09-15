using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WepWarehouse.Models;
using WepWarehouse.Services;

namespace WepWarehouse.Controllers
{
    [Authorize]
    public class CountryController : Controller
    {
        ICountryService countryService;
        public CountryController(ICountryService _countryService)
        {
            countryService = _countryService;
        }

        public IActionResult Index()
        {
            ViewData["InUpdata"] = false;
            ViewData["InValid"] = false;
            ViewData["IsEdit"] = false;
            return View("NewCountry");
        }

        public IActionResult Save(CountryDTO country )
        {
            ViewData["InUpdata"] = false;
            ViewData["InValid"] = true;
            ViewData["IsEdit"] = false;
            countryService.Insert(country);
            return View("NewCountry");
        }
        
        public IActionResult CountryList()
        {
            List<CountryDTO> countryCon = countryService.GetAllCountries();

            return View("CountryList" , countryCon);
        }

        public IActionResult Delete(int CountryID)
        {
            countryService.Delete(CountryID);
            List<CountryDTO> countryCon = countryService.GetAllCountries();

            return View("CountryList", countryCon);
        }
        
        public IActionResult Edit(int CountryId)
        {
            ViewData["InValid"] = false;
            ViewData["InUpdata"] = false;
            ViewData["IsEdit"] = true;
            CountryDTO countryEdit = countryService.GetCountry(CountryId);
            return View("NewCountry", countryEdit);
        }

        public IActionResult Update(CountryDTO countryUpdate)
        {
            ViewData["InValid"] = false;
            ViewData["InUpdata"] = true;
            ViewData["IsEdit"] = true;
            countryService.Update(countryUpdate);
            return View("NewCountry");
        }

      
    }
}
