using Entities;
using ServiceContracts1;
using ServiceContracts1.DTO;
using Microsoft.EntityFrameworkCore;

namespace CRUDTests
{
    public class CountriesService : ICountriesService
    {
        private readonly List<CountryResponse> _countries = new();
        private readonly ApplictaionDbContext? _db;

        public CountriesService()
        {
        }

        public CountriesService(ApplictaionDbContext personsDbContext, bool initialize = true)
        {
            _db = personsDbContext;
            _countries = new List<CountryResponse>
            {
            };
        }

        public Task <CountryResponse> AddCountry(CountryResponse? countryAddResponse)
        {
            throw new NotImplementedException();
        }

        public void AddCountry(CountryAddRequset? requset)
        {
            ArgumentNullException.ThrowIfNull(requset);
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
            return GetAllCountries();
        }
    }
}
