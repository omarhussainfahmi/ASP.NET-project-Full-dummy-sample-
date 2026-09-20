using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic;

namespace firstASPnet.models
{
    public class Person
    {
       public String? Name { get; set; } 
        public DateTime  Birth { get; set; }
        public string PersonName { get; internal set; }
    }
}
