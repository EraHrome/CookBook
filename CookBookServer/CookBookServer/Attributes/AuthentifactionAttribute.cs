using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CookBookServer.Attributes
{
    public class AuthentifactionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionExecutingContext)
        {
            actionExecutingContext.Result = new RedirectResult("/Home/Authorization ", true);
        }
    }
}
