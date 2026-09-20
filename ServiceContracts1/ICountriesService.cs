using Entities;
using ServiceContracts1.DTO;
using ServiceContracts1;
using System.Threading.Tasks;

namespace ServiceContracts1
{
    public interface ICountriesService

    {

       CountryResponse AddCountry(CountryResponse? countryAddResponse);
        void AddCountry(CountryAddRequset? requset);
        List<CountryResponse> GetAllCountries();

       CountryResponse? GetCountryByCountryID(Guid? CountryID);
    }
}
