using DrivingSchool.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DrivingSchool.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SchoolDbContext _schoolDbContext;

        public HomeController(ILogger<HomeController> logger, SchoolDbContext schoolDb)
        {
            _logger = logger;
            _schoolDbContext = schoolDb;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
