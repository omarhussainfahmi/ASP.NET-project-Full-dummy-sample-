using Microsoft.AspNetCore.Mvc;
using Services;
using ServiceContracts;
using ServiceContracts1.DTO;
using System.Xml.XPath;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using PersonResponse = ServiceContracts.PersonResponse;
using ServiceContracts1;
using firstASPnet.Filters.ActionFilters;
using firstASPnet.Filters.ResultFilters;
using firstASPnet.Filters.ResourceFilters;
using firstASPnet.Filters.AuthorizationFilter;
using firstASPnet.Filters.ExceptionFilters;
using firstASPnet.Filters;

namespace firstASPnet.Controllers
{
    [NonController] // Incomplete learning exercises; never expose personal records publicly.
    public class PersonsController : Controller
    {

        private readonly IPersonService _personService;
        private readonly ICountriesService _countriesService;
        private readonly object personResponse;
        private readonly ILogger<PersonsController> _logger;


        public PersonsController (IPersonService personService,
            ICountriesService countriesService
             , ILogger<PersonsController> logger
            )
        {
            _personService = personService;
            _countriesService = countriesService;
            _logger = logger;
        }
        [Route ("persons/index")]

        [TypeFilter(typeof(PersonsListActionFilter), Order =4)]
        [TypeFilter(typeof(ResponseHeaderActionFilter), Arguments = new object[] {"X-Custom-Key","Custom-Value", 1}, Order =1) ]
        [TypeFilter(typeof(PersonsListResultFilter))]
        [SkipFilter]
        public IActionResult Index(string searchBy, string? searchString, object sortBy, object sortOrder)
        {
            _logger.LogInformation("Index action method called in PersonsController");
            _logger.LogDebug("SearchBy: {searchBy}, SearchString: " +
                "{searchString}, SortBy: {sortBy}, SortOrder: {sortOrder}", searchBy, searchString, sortBy, sortOrder);
            ViewBag.SearchFields = new Dictionary<string, string>() {
                { nameof(ServiceContracts1.DTO.PersonResponse.PersonID), "PersonID" },
                { nameof(ServiceContracts1.DTO.PersonResponse.PersonName), "PersonName" },
                { nameof(ServiceContracts1.DTO.PersonResponse.Email), "Email" },
                { nameof(ServiceContracts1.DTO.PersonResponse.Age), "Age" },
                { nameof(ServiceContracts1.DTO.PersonResponse.DateOfBirth), "BirthDay" },
                { nameof(ServiceContracts1.DTO.PersonResponse.Gender), "Gender" }
            };
            List<ServiceContracts.PersonResponse> persons = _personService.getAllPersons();
            _ = _personService.GetFilteredPersons(searchBy, searchString);
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;

            _personService.GetSortedPersons(persons, sortBy, sortOrder);
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;


            return View(persons);
        }
        [HttpGet]
        [Route("persons/create")]
        [TypeFilter(typeof(PersonCreateAndEditPostActionFilter))]
        [TypeFilter(typeof(FeatureDisabledResourceFilter))]
        public IActionResult Create()
        {
            List<CountryResponse> countries = _countriesService.GetAllCountries();
            ViewBag.Countries = countries;
            ViewBag.Countries = countries.Select(temp => new SelectListItem()
            {
                Text = temp.CountryName, Value = temp.countryID.ToString()
            }
            );
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("persons/create")]
        public IActionResult Create(ServiceContracts.PersonAddRequest personAddRequest)
        {
            if (!ModelState.IsValid)
            {
                List<CountryResponse> countries = _countriesService.GetAllCountries();
                ViewBag.Countries = countries;
                ViewBag.Errors = ModelState.Values.SelectMany(ViewBag=> ViewBag.Errors).SelectMany(e => e.ErrorMessage).ToList();
                return View();
            }
            _personService.AddPerson(personAddRequest);
            return RedirectToAction("Index","Persons");

        }
        [HttpPost]
        [Route("[action]/personID")]
        [TypeFilter(typeof(PersonCreateAndEditPostActionFilter))]
        [TypeFilter(typeof(TokenAuthorizationFilter))]
        [TypeFilter(typeof(PersonAlwaysRunResultFilter))]
        public IActionResult Edit(Guid personID)
        {
        _personService.GetPersonByPersonID(personID);
            if (personResponse == null)
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        public IPersonService Get_personService()
        {
            return _personService;
        }
        [Route("PersonsCSV")]
        public async Task<IActionResult> PersonCSV(IPersonService _personService)
        {
            MemoryStream memoryStream = await _personService.GetPersonsCSV();
            return File(memoryStream, "text/csv", "persons.csv");
        }

        [Route("PersonsExcel")]
        public async Task<IActionResult> PersonExcel(IPersonService _personService)
        {
            MemoryStream memoryStream = await _personService.GetPersonExcel();
            return File(memoryStream, "text/csv", "persons.csv");
        }

        /*public async Task<IActionResult> PersonPDF(IPersonService _personService)
        {
            _personService.getAllPersons();
            List<PersonResponse> persons = await _personService.getAllPersons();
            return new ViewAsPdf("PersonPDF", persons, viewData);
        } */
    }
}
