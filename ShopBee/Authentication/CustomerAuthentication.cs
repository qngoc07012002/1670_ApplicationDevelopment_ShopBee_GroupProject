using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopBee.Repository;
using ShopBee.Repository.IRepository;

namespace ShopBee.Authentication
{
    public class CustomerAuthentication : ActionFilterAttribute
    {
        public CustomerAuthentication()
        {
            
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userRoles = context.HttpContext.Session.GetString("UserRoles");
            if (userRoles == null || !userRoles.Contains("CUSTOMER"))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    area = "Customer",
                    controller = "User",
                    action = "Login",
                }));
            }
        }
    }
}
