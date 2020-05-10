using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace AkiVeiculos.Services
{
    public class UsuarioFilterService : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.HttpContext.Session.GetInt32("Id").HasValue)
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Usuarios", action = "Login" }));
        }

    }
}