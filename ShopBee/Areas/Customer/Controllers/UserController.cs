using Microsoft.AspNetCore.Mvc;

namespace ShopBee.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class UserController : Controller
    {
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
            if (email.ToLower() == "admin" && password.ToLower() == "admin")
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else return RedirectToAction("Index");
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
