using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopBee.Repository;
using ShopBee.Repository.IRepository;

namespace ShopBee.Authentication
{
    public class StoreAuthentication : ActionFilterAttribute
    {
        public StoreAuthentication()
        {
           
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
/*            var userId = context.HttpContext.Session.GetInt32("UserId");
            if (userId == null || !_unitOfWork.User.CheckRole((int)userId, 3))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    area = "Customer",
                    controller = "User",
                    action = "AccessDenied",
                }));
            }*/
            var userRoles = context.HttpContext.Session.GetString("UserRoles");
            if (userRoles == null || !userRoles.Contains("STORE"))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    area = "Customer",
                    controller = "User",
                    action = "AccessDenied",
                }));
            }
        }
    }
}
