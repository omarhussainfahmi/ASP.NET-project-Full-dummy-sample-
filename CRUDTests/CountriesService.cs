using Entities;
using ServiceContracts1;
using ServiceContracts1.DTO;
using Microsoft.EntityFrameworkCore;

namespace CRUDTests
{
    public class CountriesService : ICountriesService
    {
        private readonly List<CountryResponse> _countries;
        private readonly ApplictaionDbContext _db;

        public CountriesService()
        {
        }

        public CountriesService(ApplictaionDbContext personsDbContext, bool initialize = true)
        {
            _db = personsDbContext;
            _countries = new List<CountryResponse>
            {
                new CountryResponse { countryID = Guid.NewGuid(), CountryName = "India" },
                new CountryResponse { countryID = Guid.NewGuid(), CountryName = "USA" },
                new CountryResponse { countryID = Guid.NewGuid(), CountryName = "UK" },
                new CountryResponse { countryID = Guid.NewGuid(), CountryName = "Canada" },
                new CountryResponse { countryID = Guid.NewGuid(), CountryName = "Australia" }
            };
        }

        public Task <CountryResponse> AddCountry(CountryResponse? countryAddResponse)
        {
            throw new NotImplementedException();
        }

        public void AddCountry(CountryAddRequset? requset)
        {
            throw new NotImplementedException();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries;
        }

        public CountryResponse? GetCountryByCountryID(Guid? CountryID)
        {
            throw new NotImplementedException();
        }

        CountryResponse ICountriesService.AddCountry(CountryResponse? countryAddResponse)
        {
            throw new NotImplementedException();
        }

        List<CountryResponse> ICountriesService.GetAllCountries()
        {
            throw new NotImplementedException();
        }
    }
}