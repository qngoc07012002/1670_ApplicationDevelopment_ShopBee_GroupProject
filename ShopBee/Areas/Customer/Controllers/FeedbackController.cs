using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ShopBee.Authentication;
using ShopBee.Models;
using ShopBee.Models.ViewModels;
using ShopBee.Repository.IRepository;

namespace ShopBee.Areas.Customer.Controllers
{
    [Area("Customer")]
    [CustomerAuthentication()]
    public class FeedbackController : Controller
    {
		private readonly IUnitOfWork _unitOfWork;
		public FeedbackController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index(int? id) 
        {
            if (id == null || id ==0) {
                return NotFound();
            }
    
            if (_unitOfWork.Feedback.Get(c=> c.OrderId == id) != null) {
				TempData["error"] = "Order Already Feedback";
				return RedirectToAction("Index", "Order", new { area = "Customer" });
			} else
            {
				int userId = int.Parse(HttpContext.Session.GetString("UserId"));
                List<OrderDetail> orderDetails = _unitOfWork.OrderDetail.GetOrderDetailsByOrder((int)id);
                List<Feedback> feedbacks = new List<Feedback>();
                foreach (OrderDetail orderDetail in orderDetails)
                {
                    Feedback feedback = new Feedback();
                    feedback.BookId = orderDetail.BookId;
                    feedback.Book = orderDetail.Book;
                    feedback.UserId = userId;
                    feedback.OrderId = orderDetail.OrderId;
                    feedback.Rating = 5;
                    feedbacks.Add(feedback);
                }
                FeedbackVM feedbackVM = new FeedbackVM();
                feedbackVM.Feedbacks = feedbacks;
				return View(feedbackVM);
			}
        }

        [HttpPost]
        public IActionResult Index(FeedbackVM feedbackVM)
        {
            foreach(Feedback feedback in feedbackVM.Feedbacks)
            {
                feedback.CreateDate = DateTime.Now;
                _unitOfWork.Feedback.Add(feedback);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index", "Order", new { area = "Customer" });
		}
    }
}
