using Microsoft.AspNetCore.Mvc;
using ShopBee.Models;
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
            Book book = _unitOfWork.Book.Get(c=> c.Id == id);
            return View(book);
        }
    }
}
