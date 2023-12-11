using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using ShopBee.Models.ViewModels;
using ShopBee.Models;
using ShopBee.Repository.IRepository;

namespace ShopBee.Areas.Admin.Controllers
{
	[Area("Admin")]
	
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
			List<Book> books = _unitOfWork.Book.GetAll("Book").ToList();
			return View(books);
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
					string bookPath = Path.Combine(wwwRootPath, "img\bookUrl");
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
					bookVM.Book.ImgUrl = @"\img\bookUrl\" + fileName;
				}
				if (bookVM.Book.Id == 0)
				{
					_unitOfWork.Book.Add(bookVM.Book);
					TempData["success"] = "Book created succesfully";
				}
				else
				{
					_unitOfWork.Book.Update(bookVM.Book);
					TempData["success"] = "Book updated succesfully";
				}
				_unitOfWork.Save();
				return RedirectToAction("Index");
			}
			else
			{
				BookVM bookVMNew = new BookVM()
				{
					MyCategories = _unitOfWork.Category.GetAll().
								Select(u => new SelectListItem
								{
									Text = u.Name,
									Value = u.Id.ToString()
								}),
					Book = new Book()
				};
				return View(bookVMNew);
			}



		}
		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Book? book = _unitOfWork.Book.Get(book => book.Id == id);
			if (book == null)
			{
				return NotFound();
			}
			return View(book);
		}
		[HttpPost]
		public IActionResult Delete(Book book)
		{
			_unitOfWork.Book.Remove(book);
			_unitOfWork.Save();
			TempData["success"] = "Book deleted succesfully";
			return RedirectToAction("Index");
		}

	}
}