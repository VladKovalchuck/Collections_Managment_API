using System.Net;
using System.Security.Claims;
using CollectionsManagmentAPI.Entity;
using CollectionsManagmentAPI.Service.Interfaces;
using CollectionsManagmentAPI.Service.Service;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Collections_Managment_API.Middleware;

public class LoginAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        var identity = context.HttpContext.User.Identity as ClaimsIdentity;
        var banned = identity?.Claims.ToList().Find(s => s.Type == "Banned").Value;
        bool.TryParse(banned, out var ban);
        if (ban)
        {
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Forbidden;
        }
    }
}