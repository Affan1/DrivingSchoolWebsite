using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrivingSchool.Models;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authorization;

namespace DrivingSchool.Controllers
{
    public class TestimonialsController : Controller
    {
        private readonly SchoolDbContext _context;
        private readonly IConfiguration _configuration;

        public TestimonialsController(SchoolDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Testimonials
        public async Task<IActionResult> Index()
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
        public async Task<IActionResult> Create([Bind("TestimonialId,StudentName,FeedBackMessage,ImageUrl")] Testimonial testimonial, IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.Length > 0)
                {
                    string connectionString = _configuration.GetConnectionString("AzureBlobStorage") ?? "Not Blob Storage connection found";
                    string containerName = "drivingschoolstorage";

                    BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

                    // Create a unique blob name
                    string blobName = $"{Guid.NewGuid()}{Path.GetExtension(upload.FileName)}";
                    BlobClient blobClient = containerClient.GetBlobClient(blobName);

                    using (var stream = upload.OpenReadStream())
                    {
                        await blobClient.UploadAsync(stream, true);
                    }

                    // Get the URI of the uploaded blob
                    string blobUri = blobClient.Uri.ToString();

                    testimonial.ImageUrl = blobUri;
                    _context.Add(testimonial);
                    await _context.SaveChangesAsync();

                    ViewBag.Message = "File uploaded successfully!";
                    ViewBag.BlobUri = blobUri; // Pass the blob URL to the view
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Message = "Please select a file to upload.";
                    ViewBag.BlobUri = null;
                }
            }
            return View(testimonial);
        }

        // GET: Testimonials/Edit/5
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TestimonialId,StudentName,FeedBackMessage,ImageUrl")] Testimonial testimonial)
        {
            if (id != testimonial.TestimonialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testimonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonialExists(testimonial.TestimonialId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(testimonial);
        }

        // GET: Testimonials/Delete/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testimonial = await _context.Testimonial.FindAsync(id);
            if (testimonial != null)
            {
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
