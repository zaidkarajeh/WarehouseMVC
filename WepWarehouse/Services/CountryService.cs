using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WepWarehouse.Data;
using WepWarehouse.Models;



namespace WepWarehouse.Services
{
    public class CountryService : ICountryService
    {
        WarehouseContext context;
        IMapper mapper;
        public CountryService(WarehouseContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }

        public void Insert(CountryDTO dTO)
        {
            Country newcountry = mapper.Map<Country>(dTO);
            context.countries.Add(newcountry);                                
            context.SaveChanges();
        }

        public List<CountryDTO> GetAllCountries()
        {

            List<Country> allCountries = context.countries.ToList();
            List<CountryDTO> countriesGet = mapper.Map<List<CountryDTO>>(allCountries);
            return countriesGet;
        }
        public void Delete( int id)
        {
            Country countryDelete = context.countries.Where(c => c.Id == id ).FirstOrDefault();
            context.countries.Remove(countryDelete);
            context.SaveChanges();
        }

        public CountryDTO GetCountry(int CountryId)
        {
            Country countryGetInfo = context.countries.Where(c => c.Id == CountryId).FirstOrDefault();
            CountryDTO countrydtogGet = mapper.Map<CountryDTO>(countryGetInfo);
            return countrydtogGet;
        }

        public void Update( CountryDTO dTO )
        {
            Country countryUpd = mapper.Map<Country>(dTO);
            context.countries.Attach(countryUpd);
            context.Entry(countryUpd).State =EntityState.Modified;
            context.SaveChanges();
        }

        




      public CountryDTO getc(int ID)
        {
            Country countryedit = context.countries.Where(c => c.Id == ID).FirstOrDefault();
            CountryDTO country = mapper.Map<CountryDTO>(countryedit);
            return country;

        }

    }
}
