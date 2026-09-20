using System;
using Entities;


namespace ServiceContracts1.DTO
{
    public class PersonAddRequest
    {
        //no ID here
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        public Person ToPerson()
        {
            return new Person() { PersonName = PersonName, Email = Email, DateOfBirth = DateOfBirth,
                Gender = Gender, CountryID = CountryID };
        }
    }
}
