using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities
{
    public class Person
    {
        internal readonly object? FirstName;
        internal readonly object? LastName;

        [Key]
        public Guid PersonID {  get; set; }
        [StringLength(40)]
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [StringLength(10)]
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        [StringLength(200)]
        public string? Address { get; set; }
        public bool ReceiveNewsLetters { get; set; }

        internal static object FromSqlRaw(string v)
        {
            throw new NotImplementedException();
        }

        public string? TIN { get; set; }

        [ForeignKey("CountryID")]
        public Country? Country { get; set; }

    }
}
