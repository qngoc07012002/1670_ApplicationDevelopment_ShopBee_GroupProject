using Microsoft.AspNetCore.Mvc;
using ShopBee.Authentication;
using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Areas.Customer.Controllers
{
    [Area("Customer")]
    [CustomerAuthentication()]
    public class StoreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public StoreController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult BecomeASeller()
        {
            var userRoles = HttpContext.Session.GetString("UserRoles");
            if (userRoles.Contains("STORE"))
            {
                return RedirectToAction("Index", "Home", new { area = "Store" });
            } else
            {
                return View();
            }
          
        }

        [HttpPost]
        public IActionResult BecomeASeller(string storeName)
        {
            ShopBee.Models.Store store = new ShopBee.Models.Store();
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
            store.Name = storeName;
            store.UserId = userId;
            store.CreateDate = DateTime.Now;
            _unitOfWork.Store.BecomeASeller(store);
            _unitOfWork.Save();
            var userRoles = _unitOfWork.User.GetUserRoles(userId);
            HttpContext.Session.SetString("UserRoles", userRoles);
            return RedirectToAction("Index", "Home", new { area = "Store" });
        }
    }
}
