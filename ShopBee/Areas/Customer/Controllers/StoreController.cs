using Microsoft.AspNetCore.Mvc;
using ShopBee.Authentication;

namespace ShopBee.Areas.Customer.Controllers
{
    [Area("Customer")]
    [CustomerAuthentication()]
    public class StoreController : Controller
    {
        public IActionResult BecomeASeller()
        {
            return View();
        }
    }
}
