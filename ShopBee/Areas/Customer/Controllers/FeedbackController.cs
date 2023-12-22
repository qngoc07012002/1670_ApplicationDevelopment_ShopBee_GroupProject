using Microsoft.AspNetCore.Mvc;

namespace ShopBee.Areas.Customer.Controllers
{
    public class FeedbackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
