using Microsoft.AspNetCore.Mvc.Filters;

namespace firstASPnet.Filters.ResultFilters
{
    public class PersonsListResultFilter : IAsyncResultFilter
    {
        private readonly ILogger<PersonsListResultFilter> _logger;

        public PersonsListResultFilter(ILogger<PersonsListResultFilter> logger)
        {
            _logger = logger;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            _logger.LogInformation("PersonsListResultFilter: OnResultExecutionAsync called.",
                nameof(PersonsListResultFilter), nameof(OnResultExecutionAsync));
            await next();
            _logger.LogInformation("PersonsListResultFilter: OnResultExecutionAsync called.",
               nameof(PersonsListResultFilter), nameof(OnResultExecutionAsync));

            context.HttpContext.Response.Headers.Add("X-PersonsListResultFilter", "Executed");
        }
    }
}
