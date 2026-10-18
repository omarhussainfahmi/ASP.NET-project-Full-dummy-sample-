using System;
using System.Collections.Generic;
using Entities;
using ServiceContracts;
using ServiceContracts1;
using ServiceContracts1.DTO;
using Services;
using EntityFrameworkCoreMock;
using Moq;
using Microsoft.EntityFrameworkCore;



namespace CRUDTests

{
    public class CountriesServiceTest
    {

        private readonly ICountriesService _countriesService;
        #region AddCountry
        #region GetAllCountries
        [Fact]
        public void GetAllCountries_EmptyList()
        {
            List<CountryResponse> actual_country_response_list = _countriesService.GetAllCountries();
            _countriesService.GetAllCountries();

            Assert.Empty(actual_country_response_list);
        }
        public CountriesServiceTest()
        {
            var countriesInitialData = new List<Country>()
            {

            };
            var dbContext = new ApplictaionDbContext(new DbContextOptionsBuilder<ApplictaionDbContext>().Options);

            _countriesService = new CountriesService(dbContext);

        }
        [Fact]
        public void Add_NULLCountry()
        {
            CountryAddRequset? requset = null;
            Assert.Throws<ArgumentNullException>(() =>
            {
                _countriesService.AddCountry(requset);
            });


        }
    }
    /*
[Fact]

    public void GetAllCountries_AddFewCountries()
    {
        List<CountryAddRequset> country_request_list = new List<CountryAddRequset>() { new CountryAddRequset() { CountryName = "USA " } };
    }*/
    #endregion
}
#endregion
