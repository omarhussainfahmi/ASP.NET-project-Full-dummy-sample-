using firstASPnet.models;
using Microsoft.AspNetCore.Mvc;

namespace firstASPnet.Models
{
    public class PersonGridModel
    {
      
        public string GridTitle { get; set; } = string.Empty;
        public List<Person> Persons { get; set; } = new List<Person>();
      
    }
}
