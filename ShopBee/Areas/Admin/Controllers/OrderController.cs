using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopBee.Models;
using ShopBee.Models.ViewModels;
using ShopBee.Repository.IRepository;

namespace ShopBee.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            List<Order> objOrderList = _unitOfWork.Order.GetAll().ToList();
            return View(objOrderList);
        }
        
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Order> obj = _unitOfWork.Order.GetAll().ToList();
            return Json(new { data = obj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var OrderDelete = _unitOfWork.Order.Get(u => u.Id == id);
            if (OrderDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Order.Remove(OrderDelete); _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion 
    }
}
