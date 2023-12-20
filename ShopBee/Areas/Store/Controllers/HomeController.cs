using Microsoft.AspNetCore.Mvc;
using ShopBee.Authentication;

namespace ShopBee.Areas.Store.Controllers
{
    [Area("Store")]
    [StoreAuthentication()]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
