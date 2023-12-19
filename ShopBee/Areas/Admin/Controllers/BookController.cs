using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using ShopBee.Models.ViewModels;
using ShopBee.Models;
using ShopBee.Repository.IRepository;
using ShopBee.Authentication;

namespace ShopBee.Areas.Admin.Controllers
{
	[Area("Admin")]
    [AdminAuthentication()]
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
			return View();
		}
		public IActionResult CreateUpdate(int? id)
		{
			BookVM bookVM = new BookVM()
			{
				MyCategories = _unitOfWork.Category.GetAll().
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
						var oldImagePath = Path.Combine(wwwRootPath, bookVM.Book.ImgUrl.TrimStart('\\'));
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
				if (bookVM.Book.Id == 0)
				{
                    bookVM.Book.CreateDate = DateTime.Today.Date;
                    bookVM.Book.ModifyDate = DateTime.Today.Date;
                    _unitOfWork.Book.Add(bookVM.Book);
					TempData["success"] = "Book created succesfully";
				}
				else
				{
                    bookVM.Book.ModifyDate = DateTime.Today.Date;
                    _unitOfWork.Book.Update(bookVM.Book);
					TempData["success"] = "Book updated succesfully";
				}
				_unitOfWork.Save();
				return RedirectToAction("Index");
			}
			else
			{

				bookVM.MyCategories = _unitOfWork.Category.GetAll().
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
        
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Book> obj = _unitOfWork.Book.GetAll(includeProperties: "Category,Store").ToList();
            return Json(new { data = obj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var bookDelete = _unitOfWork.Book.Get(u => u.Id == id);
            if (bookDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Book.Remove(bookDelete); _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
    

    }
}