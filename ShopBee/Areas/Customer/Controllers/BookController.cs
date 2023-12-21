using Microsoft.AspNetCore.Mvc;
using ShopBee.Models;
using ShopBee.Models.ViewModels;
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
    }
}
