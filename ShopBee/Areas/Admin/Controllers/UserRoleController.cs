using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopBee.Authentication;
using ShopBee.Models;
using ShopBee.Models.ViewModels;
using ShopBee.Repository;
using ShopBee.Repository.IRepository;

namespace ShopBee.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthentication()]
    public class UserRoleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webhost;

        public UserRoleController(IUnitOfWork db, IWebHostEnvironment webhost)
        {
            _unitOfWork = db;
            _webhost = webhost;
        }
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult CreateUpdate(int? id)
        {
            UserVM userVM = new UserVM()
            {
                MyUsers = _unitOfWork.User.GetAll().
                Select(u => new SelectListItem
                {
                    Text = u.Email,
                    Value = u.Id.ToString()
                }),
                MyRoles = _unitOfWork.Role.GetAll().
                Select(u => new SelectListItem
                {
                    Text = u.NomalizedName,
                    Value = u.Id.ToString()
                }),
                UserRole = new UserRole()

            };
            if (id == null || id == 0)
            {
                return View(userVM);
            }
            else
            {
                userVM.UserRole = _unitOfWork.UserRole.Get(userrole => userrole.Id == id);
                return View(userVM);
            }

        }
        [HttpPost]

        public IActionResult CreateUpdate(UserVM userVM,string? email_temp)
        {
            bool checkDuplicated = false;
            var checkEmailOfUser = _unitOfWork.User.Get(user => user.Email == email_temp);

            if (checkEmailOfUser != null && ModelState.IsValid)
            {
                List<UserRole> userRolesList = _unitOfWork.UserRole.GetAll().ToList();
                userVM.UserRole.UserId = checkEmailOfUser.Id;
                foreach (var userRole in userRolesList)
                {
                    if (userRole.UserId == userVM.UserRole.UserId && userRole.RoleId == userVM.UserRole.RoleId)
                    {
                        checkDuplicated = true;
                        break; 
                    }
                }
                if (checkDuplicated == false) 
                {
                    _unitOfWork.UserRole.Add(userVM.UserRole);
                    TempData["success"] = "User Role created succesfully";

                    _unitOfWork.Save();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "This User had this Role Before";
                    userVM.MyUsers = _unitOfWork.User.GetAll().
                    Select(u => new SelectListItem
                    {
                        Text = u.Email,
                        Value = u.Id.ToString()
                    });
                    userVM.MyRoles = _unitOfWork.Role.GetAll().
                    Select(u => new SelectListItem
                    {
                        Text = u.NomalizedName,
                        Value = u.Id.ToString()
                    });

                    return View(userVM);
                }
            }
            else
            {
                if (checkEmailOfUser == null) {
                    TempData["error"] = "This email does not belong to any User in the system";
                }
                userVM.MyUsers = _unitOfWork.User.GetAll().
                Select(u => new SelectListItem
                {
                    Text = u.Email,
                    Value = u.Id.ToString()
                });
                userVM.MyRoles = _unitOfWork.Role.GetAll().
                Select(u => new SelectListItem
                {
                    Text = u.NomalizedName,
                    Value = u.Id.ToString()
                });

                return View(userVM);
            }
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<UserRole> obj = _unitOfWork.UserRole.GetAll(includeProperties: "Role,User").ToList();
            return Json(new { data = obj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var userRole = _unitOfWork.UserRole.Get(u => u.Id == id);
            if (userRole == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.UserRole.Remove(userRole); _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
