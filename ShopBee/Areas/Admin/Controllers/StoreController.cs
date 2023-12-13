using Microsoft.AspNetCore.Mvc;
using ShopBee.Authentication;
using ShopBee.Models;
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
            List<Store> objStoreList = _unitOfWork.Store.GetAll().ToList();
            return View(objStoreList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Store obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Store.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Store? storeFromDb = _unitOfWork.Store.Get(u => u.Id == id);

            if (storeFromDb == null)
            {
                return NotFound();
            }
            return View(storeFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Store obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Store.Update(obj);
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
            List<Store> obj = _unitOfWork.Store.GetAll().ToList();
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
