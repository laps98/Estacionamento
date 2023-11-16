using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace Estacionamento.Web.Configurations;

public class RequestAuthenticationFilter : IActionFilter
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public RequestAuthenticationFilter(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var controllerType = context.Controller.GetType();
        var hasAnonymousInAction = (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        var hasAnonymousInController = controllerType.GetCustomAttribute<AllowAnonymousAttribute>() != null;

        if (hasAnonymousInController || hasAnonymousInAction)
            return;

        var session = _httpContextAccessor.HttpContext.Session.Get("_UserId");
        if (session == null)
        {
            context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Conta" }));
        }
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}
