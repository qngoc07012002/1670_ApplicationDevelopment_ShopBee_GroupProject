using Microsoft.AspNetCore.Mvc;
using ShopBee.Authentication;
using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Areas.Store.Controllers
{
    [Area("Store")]
    [StoreAuthentication()]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Category obj)
        {
            if (ModelState.IsValid)
            {
                obj.Status = 0;
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "We have received your request, please wait for the Admin team to approve this content, thank you.";
                return RedirectToAction("Index");
            }

            return View();
        }
    }


}