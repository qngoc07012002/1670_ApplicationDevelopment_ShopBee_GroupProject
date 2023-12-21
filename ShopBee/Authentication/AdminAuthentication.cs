﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopBee.Areas.Customer.Controllers;
using ShopBee.Data;
using ShopBee.Repository;
using ShopBee.Repository.IRepository;

namespace ShopBee.Authentication
{
    public class AdminAuthentication : ActionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;
        public AdminAuthentication()
        {
         
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var userRoles = context.HttpContext.Session.GetString("UserRoles");
            if (userRoles == null || !userRoles.Contains("ADMIN"))
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
