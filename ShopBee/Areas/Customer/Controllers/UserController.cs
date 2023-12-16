using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopBee.Authentication;
using ShopBee.Models;
using ShopBee.Repository;
using ShopBee.Repository.IRepository;


namespace ShopBee.Areas.Customer.Controllers
{

    [Area("Customer")]
    //[RoleAuthentication()]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webhost;
        public UserController(IUnitOfWork db, IWebHostEnvironment webhost)
        {
            _unitOfWork = db;
            _webhost = webhost;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            User user = _unitOfWork.User.Login(email, password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserAvt", user.avtURL);
                var userRoles = _unitOfWork.User.GetUserRoles(user.Id);
                HttpContext.Session.SetString("UserRoles", userRoles);
                TempData["success"] = "Login Successfully";
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
            else
            {
                TempData["error"] = "Invalid Account";
                return View();
            }
            /*else if (email.ToLower() == "customer" && password.ToLower() == "customer")
            {
                HttpContext.Session.SetString("UserRoles", "Customer");

            }*/
           ;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user, int gender, IFormFile file)
        {
            string wwwRootPath = _webhost.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string avtPath = Path.Combine(wwwRootPath, "img/userAvt");

                using (var fileStream = new FileStream(Path.Combine(avtPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                user.avtURL = @"/img/userAvt/" + fileName;
            }
            //user.avtURL = "4cc2377e-8594-43ee-9022-3f72815880dd.jpg";
            user.CreateDate = DateTime.Now;
            user.ModifyDate = DateTime.Now;
            if (gender == 0)
            {
                user.Gender = Models.User.GenderType.Male;
            }
            else if (gender == 1)
            {
                user.Gender = Models.User.GenderType.Female;
            }
            _unitOfWork.User.Add(user);
            TempData["success"] = "Account created succesfully";


            _unitOfWork.Save();
            return RedirectToAction("Login");

        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserRoles");
            HttpContext.Session.Remove("UserAvt");
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
