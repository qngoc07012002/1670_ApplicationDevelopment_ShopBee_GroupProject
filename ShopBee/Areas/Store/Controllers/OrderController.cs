using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using ShopBee.Authentication;
using ShopBee.Models;
using ShopBee.Models.ViewModels;
using ShopBee.Repository;
using ShopBee.Repository.IRepository;

namespace ShopBee.Areas.Store.Controllers
{
    [Area("Store")]
    [StoreAuthentication()]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int orderId)
        {
            var orderDetails = _unitOfWork.OrderDetail.GetAll().Where(u => u.OrderId == orderId).ToList();
            foreach (var orderDetail in orderDetails)
            {
                orderDetail.Book = _unitOfWork.Book.Get(b => b.Id == orderDetail.BookId);
            }

            OrderVM orderVM = new OrderVM()
            {
                DetailsOfOderList = orderDetails,
                Order = _unitOfWork.Order.Get(u => u.Id == orderId, includeProperties:"User"),
            };
            return View(orderVM);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string? status)
        {
            var UserIdGet = HttpContext.Session.GetString("UserId");
            int.TryParse(UserIdGet, out int storeOwnerId);
            ShopBee.Models.Store store = _unitOfWork.Store.Get(u => u.UserId == storeOwnerId);
            List<Order> obj;
            if (status == "Pending")
            {
                obj = _unitOfWork.Order.GetAll(includeProperties: "User").Where(u => u.StoreId == store.Id && u.Status== "Pending").ToList();
            }
            else if (status == "Successful")
            {
                obj = _unitOfWork.Order.GetAll(includeProperties: "User").Where(u => u.StoreId == store.Id && u.Status == "Successful").ToList();
            }
            else
            {
                obj = _unitOfWork.Order.GetAll(includeProperties: "User").Where(u => u.StoreId == store.Id).ToList();
            }
            return Json(new { data = obj });
        }

        public IActionResult Confirm(int id) 
        {
			var OrderConfirm = _unitOfWork.Order.Get(u => u.Id == id);
			
			if (OrderConfirm == null)
			{
				return Json(new { success = false, message = "Error while Comfirming" });
			}
            OrderConfirm.Status = "Successful";
			_unitOfWork.Order.Update(OrderConfirm);
			_unitOfWork.Save();
			return Json(new { success = true, message = "Confirm Successful" });
		}

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var OrderDelete = _unitOfWork.Order.Get(u => u.Id == id);
            var OrderDetailsDelete = _unitOfWork.OrderDetail.GetAll().Where(u => u.OrderId == id);
            if (OrderDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            
            foreach (var orderDetail in OrderDetailsDelete)
            {
                _unitOfWork.OrderDetail.Remove(orderDetail);
            }
            _unitOfWork.Order.Remove(OrderDelete); _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion 
    }
}