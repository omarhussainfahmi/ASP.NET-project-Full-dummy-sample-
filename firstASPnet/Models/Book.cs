using Microsoft.AspNetCore.Mvc;

namespace firstASPnet.models
{
    public class Book
    {
        //[FromQuery]
        public int? id {  get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public Book() { }
        public override string ToString()
        {
            return $"book id {id}, auther: {name}";
        }

    }
}
