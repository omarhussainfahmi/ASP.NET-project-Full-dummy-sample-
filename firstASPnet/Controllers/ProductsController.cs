using Microsoft.AspNetCore.Mvc;

namespace firstASPnet.controllers
{
    [NonController] // Incomplete learning exercises; never expose personal records publicly.
    public class ProductsController : Controller
    {
        [Route("products")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("Search")]
        public IActionResult Search(int? ProductID)
        {
            ViewBag .ProductID = ProductID;
            return View();
        }
        [Route("Order")]
        public IActionResult Order()
        {
            return View();
        }
    }
         
    }

