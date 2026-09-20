using firstASPnet.models;
using firstASPnet.Models;
using Microsoft.AspNetCore.Mvc;
namespace firstASPnet.ViewComponents
{
    //[ViewComponent]
    public class GridViewComponent : ViewComponent
    {
        public string GridTitle { get; private set; }
        public List<Person> Persons { get; private set; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            PersonGridModel model = new PersonGridModel();
            GridTitle = "Persons List";
            Persons = new List<Person>()
            {
                new Person()
                {
                    PersonName = "John"
                }
            };
            ViewData["Grid"] = model;
            return View();
        }
    }
}
