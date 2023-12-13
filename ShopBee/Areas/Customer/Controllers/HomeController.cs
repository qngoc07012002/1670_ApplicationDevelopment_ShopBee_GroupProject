using Microsoft.AspNetCore.Mvc;
using ShopBee.Authentication;
using ShopBee.Models;
using System.Diagnostics;

namespace ShopBee.Areas.Customer.Controllers
{
    [Area("Customer")]
    //[RoleAuthentication()]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            /*var userRoles = HttpContext.Session.GetString("UserRoles");
            if (userRoles == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (userRoles.Contains("Admin"))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else if (userRoles.Contains("Store"))
            {
                return RedirectToAction("Index", "Home", new { area = "Store" });
            }
            else if (userRoles.Contains("Customer"))
            {
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
*/
            // Người dùng không có vai trò hợp lệ, chuyển hướng về trang đăng nhập
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
