﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using ShopBee.Models.ViewModels;
using ShopBee.Models;
using ShopBee.Repository.IRepository;
using ShopBee.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ShopBee.Areas.Store.Controllers
{
    [Area("Store")]
    [StoreAuthentication()]
    public class BookController : Controller
    {
        //private readonly ApplicationDBContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webhost;
        public BookController(IUnitOfWork unitOfWork, IWebHostEnvironment webhost)
        {
            _unitOfWork = unitOfWork;
            _webhost = webhost;
        }
        public IActionResult Index()
        {
            var UserIdGet = HttpContext.Session.GetString("UserId");
            int.TryParse(UserIdGet, out int storeOwnerId);
            ShopBee.Models.Store store = _unitOfWork.Store.Get(u => u.UserId == storeOwnerId);
            List<Book> books = _unitOfWork.Book.GetAll(includeProperties: "Category,Store").Where(u => u.StoreID == store.Id && u.IsDeleted !=1).ToList();
            return View(books);
        }
        public IActionResult CreateUpdate(int? id)
        {
            BookVM bookVM = new BookVM()
            {
                MyCategories = _unitOfWork.Category.GetAll().Where(c => c.Status == 1).
                Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                MyStores = _unitOfWork.Store.GetAll().
                Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),

                Book = new Book()

            };
            if (id == null || id == 0)
            {
                //Create new Book
                return View(bookVM);
            }
            else
            {
                //Update a Book
                bookVM.Book = _unitOfWork.Book.Get(book => book.Id == id);
                return View(bookVM);
            }

        }
        [HttpPost]

        public IActionResult CreateUpdate(BookVM bookVM, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webhost.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string bookPath = Path.Combine(wwwRootPath, "img/bookImg");
                    if (!string.IsNullOrEmpty(bookVM.Book.ImgUrl))
                    {
                        //Delete old image
                        var oldImagePath = Path.Combine(wwwRootPath, bookVM.Book.ImgUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(bookPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    bookVM.Book.ImgUrl = @"/img/bookImg/" + fileName;
                }
                else
                {
                    BookVM bookVM2 = new();
                    bookVM2.Book = _unitOfWork.Book.Get(u => u.Id == bookVM.Book.Id);
                    bookVM.Book.ImgUrl = bookVM2.Book.ImgUrl;
                }
                if (bookVM.Book.Id == 0)
                {
                    var UserIdGet = HttpContext.Session.GetString("UserId");
                    int.TryParse(UserIdGet, out int storeOwnerId);
                    ShopBee.Models.Store store = _unitOfWork.Store.Get(u => u.UserId == storeOwnerId);
                    bookVM.Book.StoreID = store.Id;
                    bookVM.Book.CreateDate = DateTime.Today;
                    bookVM.Book.ModifyDate = DateTime.Today;
                    _unitOfWork.Book.Add(bookVM.Book);
                    TempData["success"] = "Book created succesfully";
                }
                else
                {

                    bookVM.Book.ModifyDate = DateTime.Today;
                    _unitOfWork.Book.Update(bookVM.Book);
                    TempData["success"] = "Book updated succesfully";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {

                bookVM.MyCategories = _unitOfWork.Category.GetAll().Where(c => c.Status == 1).

                            Select(u => new SelectListItem
                            {
                                Text = u.Name,
                                Value = u.Id.ToString()
                            });
                bookVM.MyStores = _unitOfWork.Store.GetAll().
                            Select(u => new SelectListItem
                            {
                                Text = u.Name,
                                Value = u.Id.ToString()
                            });
                return View(bookVM);
            }

        }
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {

            var UserIdGet = HttpContext.Session.GetString("UserId");
            int.TryParse(UserIdGet, out int storeOwnerId);
            ShopBee.Models.Store store = _unitOfWork.Store.Get(u => u.UserId == storeOwnerId);
            List<Book> books = _unitOfWork.Book.GetAll(includeProperties: "Category,Store").Where(u => u.StoreID == store.Id && u.IsDeleted == 1).ToList();

            return Json(new { data = books });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var bookDelete = _unitOfWork.Book.Get(u => u.Id == id);
            if (bookDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            bookDelete.IsDeleted = 1;
            bookDelete.Stock = 0;
            _unitOfWork.Book.Update(bookDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion

    }
}