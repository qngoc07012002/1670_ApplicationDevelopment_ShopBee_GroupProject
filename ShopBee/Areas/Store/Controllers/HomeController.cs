using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopBee.Authentication;
using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Areas.Store.Controllers
{
    [Area("Store")]
    [StoreAuthentication()]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var UserIdGet = HttpContext.Session.GetString("UserId");
            int.TryParse(UserIdGet, out int storeOwnerId);
            var store = _unitOfWork.Store.Get(u => u.UserId == storeOwnerId);
            if (store != null)
            {
                var thirtyDaysAgo = DateTime.Today.AddDays(-30);
                var ordersWithinLast30Days = _unitOfWork.Order
                    .GetAll()
                    .Where(o => o.CreateDate >= thirtyDaysAgo && o.StoreId == store.Id && o.Status == "Successful")
                    .GroupBy(o => o.CreateDate.Date) 
                    .Select(g => new
                    {
                        Date = g.Key.ToShortDateString(),
                        TotalQuantity = g.Sum(o => o.Quantity),
                        TotalEearn = g.Sum(o => o.TotalPrice)
                    })
                    .ToList();

                var days = ordersWithinLast30Days.Select(o => o.Date).ToArray();
                var data = ordersWithinLast30Days.Select(o => o.TotalQuantity).ToArray();
				var fullDateRange = Enumerable.Range(0, 30).Select(i => DateTime.Today.AddDays(-i).ToShortDateString());
				var fullData = fullDateRange.Select(date =>
				{
					var index = Array.IndexOf(days, date);
					return index != -1 ? data[index] : 0;
				}).Reverse().ToArray();

				ViewBag.Days = fullDateRange.Reverse().ToArray(); ;
				ViewBag.Data = fullData;
				ViewBag.TotalOrders = ordersWithinLast30Days.Sum(o => o.TotalQuantity);
                ViewBag.TotalEarnings30days = ordersWithinLast30Days.Sum(o => o.TotalEearn);
			}
            return View();
        }
    }
}