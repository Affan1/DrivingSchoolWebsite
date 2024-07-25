using DrivingSchool.Models;
using DrivingSchool.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace DrivingSchool.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SchoolDbContext _schoolDbContext;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger, SchoolDbContext schoolDb, IEmailService emailService)
        {
            _logger = logger;
            _schoolDbContext = schoolDb;
            _emailService = emailService;
        }
        
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Appointments appointments, EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var emailModel = new EmailModel
                    {
                        To = appointments.Email,
                        Subject = "Thank You for Contacting Drive Time Learning",
                        Body = $@"
                            <html>
                            <body style=""font-family: Arial, sans-serif; line-height: 1.6; color: #333;"">
                                <h1 style=""color: #333;"">Thank You for Contacting Drive Time Learning</h1>
                                <p>Dear {appointments.FullName},</p>
                                <p>Thank you for reaching out to Drive Time Learning. We have received your message and one of our team members will get back to you as soon as possible.</p>
                                <p>We appreciate your interest in our services and are here to assist you with any questions or concerns you may have.</p>
                                <p>We will be contacting you at the provided phone number: <strong>{appointments.PhoneNumber}</strong> or via email at: <strong>{appointments.Email}</strong>.</p>
                                <p>Additionally, we will be reaching out to you at your specified time: <strong>{appointments.DateTime.ToString()}</strong>.</p>
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
                _schoolDbContext.Appointment.Add(appointments);
                await _schoolDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else {
                return View();
            }
               
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
