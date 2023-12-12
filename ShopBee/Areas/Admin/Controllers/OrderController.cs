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
        public IActionResult CreateUpdate(int? id)
        {
            OrderVM orderVM = new OrderVM()
            {
                MyBooks = _unitOfWork.Book.GetAll().
                Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                

                Order = new Order()

            };
            if (id == null || id == 0)
            {
                //Create new Order
                return View(orderVM);
            }
            else
            {
                //Update a Order
                orderVM.Order = _unitOfWork.Order.Get(Order => Order.Id == id);
                return View(orderVM);
            }

        }
        [HttpPost]
        public IActionResult CreateUpdate(OrderVM orderVM)
        {

            if (ModelState.IsValid)
            {
                if (orderVM.Order.Id == 0)
                {
                    _unitOfWork.Order.Add(orderVM.Order);
                    TempData["success"] = "Order created succesfully";
                }
                else
                {
                    _unitOfWork.Order.Update(orderVM.Order);
                    TempData["success"] = "Order updated succesfully";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {

                orderVM.MyBooks = _unitOfWork.Category.GetAll().
                            Select(u => new SelectListItem
                            {
                                Text = u.Name,
                                Value = u.Id.ToString()
                            });
                return View(orderVM);
            }

        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Order? orderFromDb = _unitOfWork.Order.Get(u => u.Id == id);
            //OrderModel? OrderFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //OrderModel? OrderFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (orderFromDb == null)
            {
                return NotFound();
            }
            return View(orderFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Order obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Order.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Order edited successfully";
                return RedirectToAction("Index");
            }
            return View();
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
