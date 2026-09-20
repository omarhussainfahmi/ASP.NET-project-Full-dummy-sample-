using Entities;

namespace RepositoryContracts
{
    public interface ICountriesRepository
    {
        Task<Country> AddCountry (Country country);
        
        Task<List<Country>> GetAllCountries();
        Task<Country?> GetCountryById(Guid countryId);
        Task<Country?> GetCountryByName(string countryName);
        Task<Country> UpdateCountry(Country country);
    }
}
