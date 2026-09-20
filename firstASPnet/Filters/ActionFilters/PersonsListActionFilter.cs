using firstASPnet.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace firstASPnet.Filters.ActionFilters
{
    public class PersonsListActionFilter : IActionFilter
    {
        private readonly ILogger<PersonsListActionFilter> _logger;
        public PersonsListActionFilter(ILogger<PersonsListActionFilter> logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("PersonsListActionFilter: OnActionExecuted called.");
            PersonsController personsController = (PersonsController)context.Controller;
            IDictionary<string, object>? parameters = (IDictionary<string, object?>)context.HttpContext.Items["arguments"];
            if (parameters != null)
            {
                if (parameters.ContainsKey("searchString"))
                {
                    personsController.ViewData["searchString"] = parameters["searchString"];
                }
                personsController.ViewData["searchBy"] = parameters["searchBy"];
            }
            if (parameters.ContainsKey("sortBy"))
            {
                personsController.ViewData["sortBy"] = parameters["sortBy"];
            }
            if (parameters.ContainsKey("sortOrder"))
            {
                personsController.ViewData["sortOrder"] = parameters["sortOrder"];
            }
        }
        

        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items["ActionFilterExecuted"] = context.ActionArguments;
            _logger.LogInformation("PersonsListActionFilter: OnActionExecuting called.");
        }
    }
}
