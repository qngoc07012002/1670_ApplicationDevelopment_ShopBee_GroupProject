using Microsoft.AspNetCore.Mvc;
using ShopBee.Models;
using ShopBee.Repository;
using ShopBee.Repository.IRepository;

namespace ShopBee.Areas.Admin.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webhost;

        public UserRoleController(IUnitOfWork db, IWebHostEnvironment webhost)
        {
            _unitOfWork = db;
            _webhost = webhost;
        }
        public IActionResult Index()
        {
            
            return View();
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<User> obj = _unitOfWork.User.GetAll(includeProperties: "Category,Store").ToList();
            return Json(new { data = obj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var bookDelete = _unitOfWork.Book.Get(u => u.Id == id);
            if (bookDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Book.Remove(bookDelete); _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
