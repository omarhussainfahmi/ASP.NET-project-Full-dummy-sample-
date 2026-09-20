using firstASPnet.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ServiceContracts1;
using ServiceContracts1.DTO;

namespace firstASPnet.Filters.ActionFilters
{
    public class PersonCreateAndEditPostActionFilter : IAsyncActionFilter
    {
        private readonly ICountriesService _countriesService;
        private readonly ILogger<PersonCreateAndEditPostActionFilter> _logger;
        public PersonCreateAndEditPostActionFilter(ICountriesService countriesService, 
            ILogger<PersonCreateAndEditPostActionFilter> logger)
        {
            _countriesService = countriesService;
            _logger = logger;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();
            _logger.LogInformation("PersonCreateAndEditPostActionFilter: OnActionExecutionAsync called.",
                nameof(PersonCreateAndEditPostActionFilter), nameof(OnActionExecutionAsync));
        }
      
        }
    }

