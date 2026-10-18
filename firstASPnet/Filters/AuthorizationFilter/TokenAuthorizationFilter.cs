using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace firstASPnet.Filters.AuthorizationFilter;
// Fail closed. A caller-controlled cookie is never proof of authentication.
public class TokenAuthorizationFilter : IAuthorizationFilter {
    public void OnAuthorization(AuthorizationFilterContext context) {
        if (context.HttpContext.User.Identity?.IsAuthenticated != true)
            context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
    }
}
