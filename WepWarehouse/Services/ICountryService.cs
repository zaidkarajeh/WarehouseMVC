using WepWarehouse.Models;

namespace WepWarehouse.Services
{
    public interface ICountryService
    {
        void Insert(CountryDTO dTO);

       List<CountryDTO> GetAllCountries();
        void Delete(int id);

        CountryDTO GetCountry(int CountryId);

        void Update(CountryDTO dTO);
    }
}