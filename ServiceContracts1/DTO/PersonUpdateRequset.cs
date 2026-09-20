using System;
using System.Collections.Generic;
using Entities;
using System.ComponentModel.DataAnnotations;


namespace ServiceContracts1.DTO
{
    public class PersonUpdateRequset
    {
        [Required]
        public Guid PersonID { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        public Person ToPerson()
        {
            return new Person()
            {
                PersonID = PersonID,
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Gender = Gender,
                CountryID = CountryID
            };
        }
    }
}
