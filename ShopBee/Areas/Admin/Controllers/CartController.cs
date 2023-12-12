using Microsoft.AspNetCore.Mvc;

namespace ShopBee.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
