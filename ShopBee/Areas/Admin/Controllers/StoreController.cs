using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopBee.Authentication;
using ShopBee.Models;
using ShopBee.Models.ViewModels;
using ShopBee.Repository.IRepository;

namespace ShopBee.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthentication()]
    public class StoreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public StoreController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            StoreVM storeVM = new StoreVM()
            {
                MyUsers = _unitOfWork.User.GetAll().
                 Select(u => new SelectListItem
                 {
                     Text = u.Name,
                     Value = u.Id.ToString()
                 }),


                Store = new ShopBee.Models.Store()

            };
            return View(storeVM);

        }
        [HttpPost]
        public IActionResult Create(StoreVM storeVM)
        {
            if (ModelState.IsValid)
            {
                storeVM.Store.CreateDate = DateTime.Today.Date;
                _unitOfWork.Store.Add(storeVM.Store);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            else
            {

                storeVM.MyUsers = _unitOfWork.User.GetAll().
                            Select(u => new SelectListItem
                            {
                                Text = u.Name,
                                Value = u.Id.ToString()
                            });
                return View(storeVM);
            }


        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            StoreVM storeVM = new StoreVM()
            {
                MyUsers = _unitOfWork.User.GetAll().
                 Select(u => new SelectListItem
                 {
                     Text = u.Name,
                     Value = u.Id.ToString()
                 }),


                Store = new ShopBee.Models.Store()

            };
            storeVM.Store = _unitOfWork.Store.Get(store => store.Id == id);

            if (storeVM == null)
            {
                return NotFound();
            }
            return View(storeVM);
        }
        [HttpPost]
        public IActionResult Edit(StoreVM storeVM)
        {
            if (ModelState.IsValid)
            {
                storeVM.Store.CreateDate = DateTime.Today;
                _unitOfWork.Store.Update(storeVM.Store);
                _unitOfWork.Save();
                TempData["success"] = "Store edited successfully";
                return RedirectToAction("Index");
            }
            return View();
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ShopBee.Models.Store> obj = _unitOfWork.Store.GetAll(includeProperties: "User").ToList();
            return Json(new { data = obj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var storeDelete = _unitOfWork.Store.Get(u => u.Id == id);
            if (storeDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Store.Remove(storeDelete); _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion 
    }
}