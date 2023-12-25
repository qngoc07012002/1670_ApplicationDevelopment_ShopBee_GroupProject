using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopBee.Authentication;
using ShopBee.Models;
using ShopBee.Models.ViewModels;
using ShopBee.Repository;
using ShopBee.Repository.IRepository;


namespace ShopBee.Areas.Customer.Controllers
{

    [Area("Customer")]
    
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webhost;
        public UserController(IUnitOfWork db, IWebHostEnvironment webhost)
        {
            _unitOfWork = db;
            _webhost = webhost;
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
                HttpContext.Session.SetString("Cart", _unitOfWork.Cart.GetNumbersOfItems(user.Id).ToString());
                return RedirectToAction("Index", "Home", new { area = "Customer" });
            }
            else
            {
                TempData["error"] = "Invalid Account";
                return View();
            }

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
            if (_unitOfWork.User.CheckEmail(user) == false)
            {
                TempData["error"] = "Email Already Used";
                return Register();
            } else
            {
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
                _unitOfWork.User.Register(user);
                TempData["success"] = "Account created succesfully";


                _unitOfWork.Save();
                return RedirectToAction("Login");
            }
 
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserRoles");
            HttpContext.Session.Remove("UserAvt");
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }
        [CustomerAuthentication()]
        public IActionResult EditProfile()
        {
            int userId = int.Parse(HttpContext.Session.GetString("UserId"));
            User user = _unitOfWork.User.Get(b => b.Id == userId);
            return View(user);
        }
        [CustomerAuthentication()]
        [HttpPost]
        public IActionResult EditProfile(IFormFile? file, int gender, User user, string password)
        {
            if (!_unitOfWork.User.CheckPassword(user.Id, password))
            {
                TempData["error"] = "Wrong Password";
            } else
            {
                string wwwRoothPath = _webhost.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string bookPath = Path.Combine(wwwRoothPath, "img/userAvt");
                  
                        // Delete Old Image
                        var oldImagePath = Path.Combine(wwwRoothPath, user.avtURL.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }

                    using (var fileStream = new FileStream(Path.Combine(bookPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    user.avtURL = @"/img/userAvt/" + fileName;
                }
                if (gender == 0)
                {
                    user.Gender = Models.User.GenderType.Male;
                }
                else if (gender == 1)
                {
                    user.Gender = Models.User.GenderType.Female;
                }
                _unitOfWork.User.Update(user);
                HttpContext.Session.SetString("UserAvt", user.avtURL);
                _unitOfWork.Save();
                TempData["success"] = "Change Successfully";
            }
            
            return View(user);
        }
        [CustomerAuthentication()]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [CustomerAuthentication()]
        [HttpPost]
        public IActionResult ChangePassword(string currentPassword,string newPassword, string confirmNewPassword)
        {
            if (newPassword != confirmNewPassword)
            {
                TempData["error"] = "Password Doesn't Match";
            } else
            {
                int userId = int.Parse(HttpContext.Session.GetString("UserId"));
                if (!_unitOfWork.User.CheckPassword(userId, currentPassword))
                {
                    TempData["error"] = "Invalid Password";
                } else
                {
                    User user = _unitOfWork.User.Get(b => b.Id == userId);
                    user.Password = newPassword;
                    _unitOfWork.User.Update(user);
                    _unitOfWork.Save();
                    TempData["success"] = "Update Password Successfully";
                }
            }
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
