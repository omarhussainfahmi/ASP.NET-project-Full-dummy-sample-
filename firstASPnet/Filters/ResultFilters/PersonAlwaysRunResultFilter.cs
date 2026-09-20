using Microsoft.AspNetCore.Mvc.Filters;

namespace firstASPnet.Filters.ResultFilters
{
    public class PersonAlwaysRunResultFilter : IAlwaysRunResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
       
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
           if (context.Filters.OfType<SkipFilter>().Any())
            {
                return;
            }
            context.HttpContext.Response.Headers.Add("X-PersonAlwaysRunResultFilter", "Executed");
        }
    }
}
