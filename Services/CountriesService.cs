using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Entities;
using RepositoryContracts;
using System.Linq.Expressions;

namespace Services
{
    public class CountriesService : IPersonsRepository
    {
        private readonly ICountriesRepository _countriesRepository;

        public CountriesService(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
        }
        public Task<Country> AddCountry(Country country)
        {
            throw new NotImplementedException();
        }

        public Task<Person> AddPerson(Person person)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePersonByID(Guid personId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Person>> GetAllPersons()
        {
            throw new NotImplementedException();
        }

        public Task<List<Person>> GetFilteredPersons(Expression<Func<Person, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Person?> GetPersonById(Guid personId)
        {
            throw new NotImplementedException();
        }

        public Task<Person?> GetPersonByName(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }

        public Task<Person> UpdatePerson(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
