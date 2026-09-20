using Microsoft.AspNetCore.Mvc.Filters;

namespace firstASPnet.Filters.ExceptionFilters
{
    public class HandleExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<HandleExceptionFilter> _logger;
        public HandleExceptionFilter(ILogger<HandleExceptionFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "HandleExceptionFilter: OnException called.");
        }
    }
}
