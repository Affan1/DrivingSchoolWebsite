using Azure.Storage.Blobs;
using DrivingSchool.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrivingSchool.Controllers
{
    public class TestimonialsController : Controller
    {
        private readonly SchoolDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public TestimonialsController(SchoolDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _environment = webHostEnvironment;
        }
        // GET: Testimonials
        public async Task<IActionResult> Index()
        {
            return View(await _context.Testimonial.ToListAsync());
        }
        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            return View(await _context.Testimonial.ToListAsync());
        }
        // GET: Testimonials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonial
                .FirstOrDefaultAsync(m => m.TestimonialId == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // GET: Testimonials/Create
        [Authorize]
        public IActionResult Create()
        { 
             return View();
        }

        // POST: Testimonials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Testimonial testimonial, IFormFile upload)
        {
            if (upload == null || upload.Length == 0)
            {
                ModelState.AddModelError("upload", "Image is required.");
            }

            if (ModelState.IsValid)
            {
                if (upload != null && upload.Length > 0)
                {
                    // Folder path: wwwroot/uploads/testimonials
                    string uploadsFolder = Path.Combine(
                        _environment.WebRootPath,
                        "uploads",
                        "testimonials");

                    // Create directory if it doesn't exist
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Unique file name
                    string fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName);
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    // Save file locally
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await upload.CopyToAsync(fileStream);
                    }

                    // Save relative path in DB
                    testimonial.ImageUrl = "/uploads/testimonials/" + fileName;
                }

                _context.Add(testimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(testimonial);
        }

        // GET: Testimonials/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonial.FindAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }
            return View(testimonial);
        }

        // POST: Testimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Testimonial testimonial)
        {
            if (id != testimonial.TestimonialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // If new image uploaded
                    if (testimonial.ImageFile != null)
                    {
                        string uploadsFolder = Path.Combine(
                            _environment.WebRootPath, "uploads");

                        Directory.CreateDirectory(uploadsFolder);

                        string uniqueFileName =
                            Guid.NewGuid() + Path.GetExtension(testimonial.ImageFile.FileName);

                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await testimonial.ImageFile.CopyToAsync(stream);
                        }

                        // Save new image path
                        testimonial.ImageUrl = "/uploads/" + uniqueFileName;
                    }
                    // else → keep existing ImageUrl automatically

                    _context.Update(testimonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonialExists(testimonial.TestimonialId))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(testimonial);
        }

        // GET: Testimonials/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonial = await _context.Testimonial
                .FirstOrDefaultAsync(m => m.TestimonialId == id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        // POST: Testimonials/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testimonial = await _context.Testimonial.FindAsync(id);
            if (testimonial != null)
            {
                if (!string.IsNullOrEmpty(testimonial.ImageUrl))
                {
                    string imagePath = Path.Combine(
                        _environment.WebRootPath,
                        testimonial.ImageUrl.TrimStart('/'));

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                _context.Testimonial.Remove(testimonial);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestimonialExists(int id)
        {
            return _context.Testimonial.Any(e => e.TestimonialId == id);
        }
    }
}
