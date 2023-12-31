﻿using Microsoft.AspNetCore.Mvc;
using ShopBee.Authentication;
using ShopBee.Models;
using ShopBee.Repository.IRepository;
using System.Linq;

namespace ShopBee.Areas.Store.Controllers
{
    [Area("Store")]
    [StoreAuthentication()]
    public class FeedbackController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webhost;
        public FeedbackController(IUnitOfWork unitOfWork, IWebHostEnvironment webhost)
        {
            _unitOfWork = unitOfWork;
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

            var UserIdGet = HttpContext.Session.GetString("UserId");
            int.TryParse(UserIdGet, out int storeOwnerId);
            ShopBee.Models.Store store = _unitOfWork.Store.Get(u => u.UserId == storeOwnerId);
            List<int> bookIds = _unitOfWork.Book.GetAll().Where(u => u.StoreID == store.Id).Select(b => b.Id).ToList();
            List<Feedback> feedbacks = _unitOfWork.Feedback.GetAll(includeProperties: "Book").Where(u => bookIds.Contains((int)u.BookId)).ToList();

            return Json(new { data = feedbacks });
        }

        [HttpPost]
        public IActionResult Update(int id, string? response)
        {
            Feedback feedback = _unitOfWork.Feedback.Get(u => u.Id == id);
            if (feedback != null)
            {
                feedback.Response = response;
                _unitOfWork.Feedback.Update(feedback);
                _unitOfWork.Save();
                return Json(new { success = true, message = "Update Response Successful" });
            }
            else
            {
                return Json(new { success = false, message = "Error while Update" });
            }
        }
        #endregion

    }
}