using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Estacionamento.Web.Configurations;

public class RequestAuthenticationFilter : IActionFilter
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public RequestAuthenticationFilter(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    /// 

    /// OnActionExecuting
    /// 

    /// 
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var session = _httpContextAccessor.HttpContext.Session.Get("UserId");
        if (session == null)
        {
            context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Conta" }));
        }
    }
    /// 

    /// OnActionExecuted
    /// 

    /// 
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
