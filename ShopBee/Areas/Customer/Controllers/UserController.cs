using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopBee.Authentication;
using ShopBee.Models;
using ShopBee.Repository;
using ShopBee.Repository.IRepository;


namespace ShopBee.Areas.Customer.Controllers
{

    [Area("Customer")]
    //[RoleAuthentication()]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            User user = _unitOfWork.User.Login(email, password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetInt32("UserId", user.Id);
                var userRoles = _unitOfWork.User.GetUserRoles(user.Id);
                HttpContext.Session.SetString("UserRoles", userRoles);
                TempData["success"] = "Login Successfully";
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
            else
            {
                TempData["error"] = "Invalid Account";
                return View();
            }
            /*else if (email.ToLower() == "customer" && password.ToLower() == "customer")
            {
                HttpContext.Session.SetString("UserRoles", "Customer");

            }*/
           ;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserRoles");
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
