using Microsoft.AspNetCore.Mvc;
using ShopBee.Authentication;
using ShopBee.Data;
using ShopBee.Models;
using ShopBee.Models.ViewModels;
using ShopBee.Repository.IRepository;

namespace ShopBee.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthentication()]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            int numberOfBooks = _unitOfWork.Book.GetNumberOfBooks();
            int numberOfUsers = _unitOfWork.User.GetNumberOfUsers();
            int numberOfOrders = _unitOfWork.Order.GetNumberOfOrders();
            int numberOfStores = _unitOfWork.Store.GetNumberOfStores();

            ViewBag.BookModel = new BookVM { NumberOfBooks = numberOfBooks };
            ViewBag.UserModel = new UserVM { NumberOfUsers = numberOfUsers };
            ViewBag.OrderModel = new OrderVM { NumberOfOrders = numberOfOrders };
            ViewBag.StoreModel = new StoreVM { NumberOfStores = numberOfStores };

            return View();
        }

    }
}
