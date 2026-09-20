using Microsoft.AspNetCore.Mvc;
using firstASPnet.models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http.HttpResults;
using ServiceContracts;
namespace Services { 
namespace firstASPnet.controllers
       

{
    public class HomeController : Controller
    {
          

            //[Controller]
            public class homeController: Controller
        {
                private readonly IConfiguration _configuration;

                private readonly CitiesService citiesService;
                private readonly IWebHostEnvironment _webHostEnvironment;
                /*
                [Route("hello1")]
                [Route("/")]
                public IActionResult index1(Person person)
                {
                    if (ModelState.IsValid) {
                        return BadRequest();
                    }
               return Content($"{person}");
                }
                [Route("hello2")]
                public ContentResult index2()
                {
                    return new ContentResult() {Content= "hello  method2", ContentType = "text/plain" };
                }
                [Route("hello3/{mobile:regex(^\\d{{10}}$)}")]
                public string index3()
                {
                    return "hello  method3";
                }
              */
                // [Route("home")]
                /*public IActionResult Index()
                {
                    ViewData["appTitle"] = "Asp.Net Core Demo App";
                    return View();
                    //return new ViewResult() { ViewName = "Home" };
                } 
                [Route("/")]
                public IActionResult Index ()
                { 
                    return View(); 
                }
                [Route("company")]
                public IActionResult About () 
                { 
                    return View();
                }
                [Route("contact")]
                public IActionResult Contact ()
                { 
                       return View();
                } 
                */


                /*public HomeController (IWebHostEnvironment webHostEnvironment) 
                {
                 _webHostEnvironment = webHostEnvironment;
               
                } */

                [Route("/")]
            public IActionResult Index()
            {
                   // ViewBag.CurrentEnviornment = _webHostEnvironment;
                ViewData["ListTitle"] = "Cities";
                ViewData["ListItems"] = new List<string>()
                {
                   "London",
                    "Paris",
                    "New York"
                };
                    ViewBag.CleintID = _configuration["weatherapi:ClientID"];
                    ViewBag.CleintSecret = _configuration.GetValue("weatherapi:CleintSecret", "the default client secret");
                    return View();
            }
            [Route("company")]
            public IActionResult About()
            {
                return View();
            }
            [Route("contact")]
            public IActionResult Contact()
            {
                return View();
            }
        }
}
}
}