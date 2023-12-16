using Microsoft.AspNetCore.Mvc;
using ShopBee.Authentication;
using ShopBee.Models;
using ShopBee.Repository.IRepository;
using System.Diagnostics;

namespace ShopBee.Areas.Customer.Controllers
{
    [Area("Customer")]
    //[RoleAuthentication()]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }

        public IActionResult Index()
        {
            /*var userRoles = HttpContext.Session.GetString("UserRoles");
            if (userRoles == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (userRoles.Contains("Admin"))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else if (userRoles.Contains("Store"))
            {
                return RedirectToAction("Index", "Home", new { area = "Store" });
            }
            else if (userRoles.Contains("Customer"))
            {
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
*/
            // Người dùng không có vai trò hợp lệ, chuyển hướng về trang đăng nhập
            return View();
        }

        [HttpGet]
        public IActionResult GetAllBook()
        {
            List<Book> obj = _unitOfWork.Book.GetAll().ToList();
            return Json(obj);
        }
    }
}
