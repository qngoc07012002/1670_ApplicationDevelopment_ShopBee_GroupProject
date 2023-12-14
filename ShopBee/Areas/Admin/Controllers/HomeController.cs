using Microsoft.AspNetCore.Mvc;
using ShopBee.Authentication;

namespace ShopBee.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthentication()]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
