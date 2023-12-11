using Microsoft.AspNetCore.Mvc;

namespace ShopBee.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
