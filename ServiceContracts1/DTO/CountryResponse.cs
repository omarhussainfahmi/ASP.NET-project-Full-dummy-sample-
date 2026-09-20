using System;
using Entities;

namespace ServiceContracts1.DTO
{
    public class CountryResponse
    {
        public Guid countryID {  get; set; }
        public string? CountryName { get; set; }
    }
    public static class CountryExtenstions
    {
        public static CountryResponse ToCountryResponse (this Country country)
        {
            return new CountryResponse() { countryID = country.CountryID, CountryName = country.CountryName };
        }
    }
}
