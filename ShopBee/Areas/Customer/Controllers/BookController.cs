using Microsoft.AspNetCore.Mvc;
using ShopBee.Models;
using ShopBee.Models.ViewModels;
using ShopBee.Repository;
using ShopBee.Repository.IRepository;

namespace ShopBee.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            BookDetailVM bookDetailVM = new BookDetailVM();
            bookDetailVM.book = _unitOfWork.Book.Get(c=> c.Id == id,includeProperties: "Store,Category");
            bookDetailVM.feedbacks = _unitOfWork.Feedback.GetFeedbackByBook(bookDetailVM.book.Id);
            return View(bookDetailVM);
        }
        [HttpPost]
        public IActionResult Details(int storeId, int bookId, int quantity)
        {
            if (bookId == null || bookId == 0)
            {
                return NotFound();
            }  
            string strUserId = HttpContext.Session.GetString("UserId");
            
            if (string.IsNullOrEmpty(strUserId)) {
                return RedirectToRoute(new RouteValueDictionary(new
                {
                    area = "Customer",
                    controller = "User",
                    action = "Login",
                }));
            } else
            {
                if (quantity == 0)
                {
                    TempData["error"] = "Out of Stock";
                    return RedirectToAction("Details", bookId);
                }
                int userId = int.Parse(strUserId);
                Cart cart = new Cart();
                cart.StoreID = storeId;
                cart.BookID = bookId;
                cart.UserID = userId;
                cart.Quantity = quantity;

                _unitOfWork.Cart.Add(cart);
                _unitOfWork.Save();


                HttpContext.Session.SetString("Cart", _unitOfWork.Cart.GetNumbersOfItems(userId).ToString());
                BookDetailVM bookDetailVM = new BookDetailVM();
                bookDetailVM.book = _unitOfWork.Book.Get(c => c.Id == bookId, includeProperties: "Store,Category");
                bookDetailVM.feedbacks = _unitOfWork.Feedback.GetFeedbackByBook(bookDetailVM.book.Id);
                return RedirectToAction("Details", bookId);
            }
        }
    }
}
