using DrivingSchool.Models;
using DrivingSchool.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;

namespace DrivingSchool.Controllers
{
    public class ContactController : Controller
    {
        private readonly SchoolDbContext _context;
        private readonly IEmailService _emailService;

        public ContactController(SchoolDbContext dbContext, IEmailService emailService)
        {
            _context = dbContext;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Index(Questions question)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var emailModel = new EmailModel
                    {
                        To = question.Email,
                        Subject = "Thank You for Contacting Drive Time Learning",
                        Body = $@"
                            <html>
                            <body style=""font-family: Arial, sans-serif; line-height: 1.6; color: #333;"">
                                <h1 style=""color: #333;"">Thank You for Contacting Drive Time Learning</h1>
                                <p>Dear {question.FullName},</p>
                                <p>Thank you for reaching out to Drive Time Learning. We have received your message and one of our team members will get back to you as soon as possible.</p>
                                <p>We appreciate your interest in our services and are here to assist you with any questions or concerns you may have.</p>
                                <p>We will be contacting you at the provided phone number: <strong>{question.PhoneNumber}</strong> or via email at: <strong>{question.Email}</strong>.</p>
                                <p>Best regards,</p>
                                <p>Drive Time Learning Team</p>
                                <p style=""font-size: small; color: #777;"">This email was sent on {DateTime.Now.ToString("f", CultureInfo.CreateSpecificCulture("en-US"))}</p>
                            </body>
                            </html>"
                    };

                    await _emailService.SendEmailAsync(emailModel.To, emailModel.Subject, emailModel.Body);

                    ViewBag.Message = "Email sent successfully!";
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Some Error: " + ex.Message;
                }
                question.Created = DateTime.Now;
                _context.Questions.Add(question);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }


            return View();
        }
    }
}
