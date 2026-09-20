using System;
using System.Collections.Generic;
using Entities;

namespace ServiceContracts1.DTO
{
    public class CountryAddRequset
    {
        public string? CountryName { get; set; }
        public Country ToCountry()
        {
            return new Country()
            {
                CountryName =
                CountryName
            };
        }
    }
}
