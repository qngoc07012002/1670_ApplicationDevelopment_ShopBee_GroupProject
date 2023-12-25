using Microsoft.AspNetCore.Mvc;
using ShopBee.Authentication;
using ShopBee.Models;
using ShopBee.Models.ViewModels;
using ShopBee.Repository.IRepository;

namespace ShopBee.Areas.Customer.Controllers
{
    [Area("Customer")]
    [CustomerAuthentication()]
    public class OrderController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
            OrderUserVM vm = new OrderUserVM();
            vm.orders = _unitOfWork.Order.GetOrderByUser(userId);
            foreach (Order order in vm.orders)
            {
                List<OrderDetail> orderDetails = _unitOfWork.OrderDetail.GetOrderDetailsByOrder(order.Id);
                vm.ordersDetail.Add(orderDetails);
            }
            return View(vm);
        }
        
        public IActionResult Checkout()
        {
          
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
            int numberItem = _unitOfWork.Cart.GetNumbersOfItems(userId);
            if (numberItem == 0)
            {
              
                return RedirectToAction("Index","Cart", new {area = "Customer"});
            }
            List<Cart> carts = _unitOfWork.Cart.GetCartByUser(userId);
            List<List<Cart>> cartsFilter = carts.GroupBy(cart => cart.StoreID).Where(group => group.Count() >= 1) .Select(group => group.ToList()).ToList();
            foreach (List<Cart> listcart in cartsFilter)
            {
                Order order = new Order();
                order.UserId = userId;
               
                order.Quantity = numberItem;
                order.TotalPrice = 0;
                foreach (Cart cart in listcart)
                {
                    order.StoreId = (int)cart.StoreID;
                    order.TotalPrice = (decimal)(order.TotalPrice + (cart.Quantity * cart.Book.DiscountPrice));
                    _unitOfWork.Cart.Remove(cart);
                }
                order.Method = "Paypal";
                order.Status = "Pending";
                order.CreateDate = DateTime.Now;
                int orderId = _unitOfWork.Order.CreateOrder(order);
                foreach (var cart in listcart)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.BookId = cart.BookID;
                    orderDetail.OrderId = orderId;
                    orderDetail.Quantity = (int)cart.Quantity;
                    orderDetail.TotalPrice = (double)(orderDetail.Quantity * cart.Book.DiscountPrice);

                    Book book = _unitOfWork.Book.Get(c => c.Id == cart.BookID);
                    book.Stock = book.Stock - cart.Quantity;
                    _unitOfWork.Book.Update(book);

                    _unitOfWork.OrderDetail.Add(orderDetail);
                }
            }

            HttpContext.Session.SetString("Cart", _unitOfWork.Cart.GetNumbersOfItems(userId).ToString());
           
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
