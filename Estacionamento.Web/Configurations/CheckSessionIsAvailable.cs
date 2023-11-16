﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Estacionamento.Web.Configurations;

public class CheckSessionIsAvailable : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        base.OnActionExecuting(filterContext);
        if (filterContext.HttpContext == null || filterContext.HttpContext.Session.GetString("UserID") == null)
        {
            //return RedirectToAction("Index", "Login");
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                controller = "Login",
                action = "Index"
            }));
        }
    }
}