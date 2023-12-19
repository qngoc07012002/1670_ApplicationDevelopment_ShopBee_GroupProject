using Microsoft.AspNetCore.Mvc;

namespace ShopBee.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
