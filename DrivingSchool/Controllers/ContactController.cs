﻿using Microsoft.AspNetCore.Mvc;

namespace DrivingSchool.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
