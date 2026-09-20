using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Entities;
using RepositoryContracts;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace Repositories
{
    public class PersonsRepository : IPersonsRepository
    {
        private readonly ApplictaionDbContext _db;
        private readonly ILogger<PersonsRepository> _logger;

        public PersonsRepository(ApplictaionDbContext db, ILogger<PersonsRepository> logger)

        {
            _db = db;
            _logger = logger;
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
            _logger.LogInformation("Getting filtered persons from the database");
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
