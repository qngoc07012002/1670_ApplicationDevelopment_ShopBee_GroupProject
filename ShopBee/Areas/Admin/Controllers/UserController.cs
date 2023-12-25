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
            List<User> objUserList = _unitOfWork.User.GetAll().ToList();
            return View(objUserList);
        }
        public IActionResult CreateUpdate(int? id)
        {
            UserVM userVM = new UserVM()
            {
                MyUsers = _unitOfWork.User.GetAll().
                Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                User = new User()

            };
            if ( id == null || id == 0)
            {
                //Create new User
                return View(userVM);
            }
            else
            {
                //Update a User
                userVM.User = _unitOfWork.User.Get(user => user.Id == id);
                return View(userVM);
            }
        }
        [HttpPost]
        public IActionResult CreateUpdate(UserVM userVM, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webhost.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string userPath = Path.Combine(wwwRootPath, "img/userAvt");
                    if (!string.IsNullOrEmpty(userVM.User.avtURL))
                    {
                        //Delete old image
                        var oldImagePath = Path.Combine(wwwRootPath, userVM.User.avtURL.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(userPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    userVM.User.avtURL = @"/img/userAvt/" + fileName;
                }
                if (userVM.User.Id == 0)
                {
                    userVM.User.CreateDate = DateTime.Today.Date;
                    userVM.User.ModifyDate = DateTime.Today.Date;
                    _unitOfWork.User.Register(userVM.User);
                    TempData["success"] = "User created succesfully";
                }
                else
                {
                    userVM.User.ModifyDate = DateTime.Today.Date;
                    _unitOfWork.User.Update(userVM.User);
                    TempData["success"] = "User updated succesfully";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                UserVM userVMNew = new UserVM()
                {
                    MyUsers = _unitOfWork.User.GetAll().
                                Select(u => new SelectListItem
                                {
                                    Text = u.Name,
                                    Value = u.Id.ToString()
                                }),
                    User = new User()
                };
                return View(userVMNew);
            }
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            User? userFromDb = _unitOfWork.User.Get(u => u.Id == id);
            
            if (userFromDb == null)
            {
                return NotFound();
            }
            return View(userFromDb);
        }
        [HttpPost]
        public IActionResult Edit(User obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.User.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "User edited successfully";
                return RedirectToAction("Index");
            }
            return View();
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<User> obj = _unitOfWork.User.GetAll().ToList();
            return Json(new { data = obj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var userDelete = _unitOfWork.User.Get(u => u.Id == id);
            if (userDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.User.Remove(userDelete); _unitOfWork.Save();
            return Json(new { success = true, message = "Delete User Successful" });
        }
        #endregion 
    }
}

