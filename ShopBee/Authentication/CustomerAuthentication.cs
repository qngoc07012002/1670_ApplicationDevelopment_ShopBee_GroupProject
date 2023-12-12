using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopBee.Repository;
using ShopBee.Repository.IRepository;

namespace ShopBee.Authentication
{
    public class CustomerAuthentication : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerAuthentication()
        {
            
        }
        public CustomerAuthentication(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var userId = context.HttpContext.Session.GetInt32("UserId");
            if (userId == null || !_unitOfWork.User.CheckRole((int)userId, 1))
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
