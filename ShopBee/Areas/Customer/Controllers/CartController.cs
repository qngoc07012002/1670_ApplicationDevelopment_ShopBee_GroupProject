using Microsoft.AspNetCore.Mvc;

namespace ShopBee.Areas.Customer.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
