using Microsoft.AspNetCore.Mvc;

namespace DrivingSchool.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
