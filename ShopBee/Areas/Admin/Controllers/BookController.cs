﻿using Microsoft.AspNetCore.Mvc;

namespace ShopBee.Areas.Admin.Controllers
{
    public class BookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
