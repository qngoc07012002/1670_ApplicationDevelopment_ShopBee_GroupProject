using Microsoft.AspNetCore.Mvc;
using ShopBee.Authentication;
using ShopBee.Models;
using ShopBee.Models.ViewModels;
using ShopBee.Repository.IRepository;
using System.Diagnostics;

namespace ShopBee.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }

        public IActionResult Index(string? searchString)
        {
            HomeVM homeVM = new HomeVM();
            homeVM.categories = _unitOfWork.Category.GetAllCategory().ToList();
        
            if (string.IsNullOrEmpty(searchString))
            {
                homeVM.books = _unitOfWork.Book.GetAll().Where(c => c.IsDeleted != 1).ToList();
            } else
            {
                homeVM.books = _unitOfWork.Book.GetBookBySearch(searchString).Where(c => c.IsDeleted != 1).ToList();
            }
            return View(homeVM);
        }

       

        public IActionResult FilterByCategory(int? id)
        {   
            if (id == null || id == 0) {
                return NotFound();
            }
            HomeVM homeVM = new HomeVM();
            homeVM.categories = _unitOfWork.Category.GetAllCategory().ToList();
            
            homeVM.books = _unitOfWork.Book.GetAllBookByCategory(id).Where(c => c.IsDeleted != 1).ToList();
            return View("Index", homeVM);
        }
        public IActionResult FilterByPrice(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            HomeVM homeVM = new HomeVM();
            homeVM.categories = _unitOfWork.Category.GetAllCategory().ToList();
            homeVM.books = _unitOfWork.Book.GetAllBookSort().Where(c => c.IsDeleted != 1).ToList(); 
            if (id == 2)
            {
                homeVM.books.Reverse();
            }
       
        
            return View("Index", homeVM);
        }
 
        
    }
}
