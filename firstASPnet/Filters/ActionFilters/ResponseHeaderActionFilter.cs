using Microsoft.AspNetCore.Mvc.Filters;

namespace firstASPnet.Filters.ActionFilters
{
    public class ResponseHeaderActionFilter : IAsyncActionFilter, IOrderedFilter
    {
        private readonly ILogger<ResponseHeaderActionFilter> _logger;
        private readonly string _key;
        private readonly string _value;

        public int Order { get; set; }

        public ResponseHeaderActionFilter(ILogger<ResponseHeaderActionFilter> logger, string key, string value, int order)
        {
            _logger = logger;
            key = key;
            value = value;
            Order = order;
        }

        public ResponseHeaderActionFilter(ILogger<ResponseHeaderActionFilter> logger, string v1, string v2)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
 _logger.LogInformation("ResponseHeaderActionFilter: OnActionExecuted called.");
            context.HttpContext.Response.Headers.
                Add("X-Response-Header", "This is a custom response header added by ResponseHeaderActionFilter.");
       context.HttpContext.Response.Headers.Add(_key, _value);
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           await next();
            _logger.LogInformation("ResponseHeaderActionFilter: OnActionExecutionAsync called.");
            context.HttpContext.Response.Headers
                .Add("X-Response-Header", "This is a custom response header added by ResponseHeaderActionFilter.");
            context.HttpContext.Response.Headers.Add(_key, _value);

        }
    }
}
