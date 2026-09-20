using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Entities
{
    public class ApplictaionDbContext : DbContext
    {
        public ApplictaionDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Country> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<Person>().ToTable("Persons");

            modelBuilder.Entity<Country>().HasData(
                new Country { CountryID = Guid.NewGuid(), CountryName = "India" },
                new Country { CountryID = Guid.NewGuid(), CountryName = "USA" },
                new Country { CountryID = Guid.NewGuid(), CountryName = "UK" },
                new Country { CountryID = Guid.NewGuid(), CountryName = "Canada" },
                new Country { CountryID = Guid.NewGuid(), CountryName = "Australia" }
            );

        }
        public List<Person> sp_GetAllPersons()
        {
            // This method is intended to represent a stored procedure call to get all persons.
            // Implementation will depend on the specific database and ORM being used.

            //return Person.FromSqlRaw("EXECUTE [dbo].[GetAllPersons]").ToList();
            throw new NotImplementedException("This method should call the stored procedure to get all persons.");
        }
        public int sp_InsertPerson(Person person)
        {
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@FirstName", person.FirstName ?? (object)DBNull.Value),
                new SqlParameter("@LastName", person.LastName ?? (object)DBNull.Value),
                new SqlParameter("@CountryID", person.CountryID ?? (object)DBNull.Value)
            };
            // This method is intended to represent a stored procedure call to insert a person.
            // Implementation will depend on the specific database and ORM being used.
            //return Database.ExecuteSqlRaw("EXECUTE [dbo].[InsertPerson] @FirstName, @LastName, @CountryID", 
            //    new SqlParameter("@FirstName", person.FirstName),
            //    new SqlParameter("@LastName", person.LastName),
            //    new SqlParameter("@CountryID", person.CountryID))
            return Database.ExecuteSqlRaw("EXECUTE [dbo].[InsertPerson] @FirstName, @LastName, @CountryID", parameters);
          
        }
    }
}

