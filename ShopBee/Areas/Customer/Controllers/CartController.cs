using Microsoft.AspNetCore.Mvc;
using ShopBee.Authentication;
using ShopBee.Models;
using ShopBee.Models.ViewModels;
using ShopBee.Repository.IRepository;

namespace ShopBee.Areas.Customer.Controllers
{
    [Area("Customer")]
    [CustomerAuthentication()]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartController(IUnitOfWork db)
        {
            _unitOfWork = db;
        }
        public IActionResult Index()
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
            CartVM cartVM = new CartVM();
            cartVM.carts = _unitOfWork.Cart.GetCartByUser(userId);
            foreach (var cart in cartVM.carts)
            {
                cartVM.totalPrice = (decimal)(cartVM.totalPrice + (cart.Quantity * cart.Book.DiscountPrice));
            }
            return View(cartVM);
        }

  

        public IActionResult RemoveBookToCart(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Cart cart = _unitOfWork.Cart.Get(c=> c.Id == id);
            _unitOfWork.Cart.Remove(cart);
            _unitOfWork.Save();
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
            HttpContext.Session.SetString("Cart", _unitOfWork.Cart.GetNumbersOfItems(userId).ToString());
            return RedirectToAction("Index");
        }
    }
}
