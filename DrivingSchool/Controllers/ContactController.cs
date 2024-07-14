using DrivingSchool.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DrivingSchool.Controllers
{
    public class ContactController : Controller
    {
        private readonly SchoolDbContext _context;

        public ContactController(SchoolDbContext dbContext)
        {
            _context = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Index(Questions question)
        {
            if (ModelState.IsValid)
            {
                question.Created = DateTime.Now;
                _context.Questions.Add(question);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }


            return View();
        }
    }
}
