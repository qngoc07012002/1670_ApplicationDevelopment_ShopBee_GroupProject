using Microsoft.AspNetCore.Mvc;

namespace ShopBee.Areas.Customer.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
