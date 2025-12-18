using DrivingSchool.Models;
using DrivingSchool.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Serilog;
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
        public async Task<IActionResult> Index(Appointments appointments)
        {
            _logger.LogInformation(
                 "Appointment submission started for {Email}",
                        appointments?.Email);
            if (ModelState.IsValid)
            {
                try
                {
                    var adminEmail = new EmailModel
                    {
                        To = "usman-majeed@drivetimelearning.co.uk",
                        Subject = "New Appointment Request | Drive Time Learning",
                        Body = $@"
    <html>
    <body style='font-family: Arial, sans-serif; color:#333; line-height:1.6;'>
        <h2 style='color:#0b3d91;'>New Appointment Request</h2>

        <table style='border-collapse: collapse; width:100%;'>
            <tr>
                <td style='padding:8px; font-weight:bold;'>Full Name:</td>
                <td style='padding:8px;'>{appointments.FullName}</td>
            </tr>
            <tr style='background-color:#f5f5f5;'>
                <td style='padding:8px; font-weight:bold;'>Email:</td>
                <td style='padding:8px;'>{appointments.Email}</td>
            </tr>
            <tr>
                <td style='padding:8px; font-weight:bold;'>Phone Number:</td>
                <td style='padding:8px;'>{appointments.PhoneNumber}</td>
            </tr>
<tr>
                <td style='padding:8px; font-weight:bold;'>Full Address:</td>
                <td style='padding:8px;'>{appointments.Address}</td>
            </tr>
<tr>
                <td style='padding:8px; font-weight:bold;'>Postal Code:</td>
                <td style='padding:8px;'>{appointments.PostalCode}</td>
            </tr>
            <tr style='background-color:#f5f5f5;'>
                <td style='padding:8px; font-weight:bold;'>Preferred Contact Time:</td>
                <td style='padding:8px;'>{appointments.Description}</td>
            </tr>
        </table>

        <p style='margin-top:20px;'>
            Please follow up with the client at your earliest convenience.
        </p>

        <p style='font-size:12px; color:#777;'>
            This email was generated automatically on {DateTime.Now:f}
        </p>
    </body>
    </html>"
                    };
                    // ADMIN EMAIL LOGGER
                    _logger.LogInformation(
                        "Sending admin appointment email for {Email}",
                        appointments.Email);

                    // SEND TO usman-majeed@drivetimelearning.co.uk
                    await _emailService.SendEmailAsync(
                        adminEmail.To,
                        adminEmail.Subject,
                        adminEmail.Body
                    );

                    // EMAIL TO CLIENT
                    var clientEmail = new EmailModel
                    {
                        To = appointments.Email,
                        Subject = "Thank You for Contacting Drive Time Learning",
                        Body = $@"
    <html>
    <body style='font-family: Arial, sans-serif; color:#333; line-height:1.6;'>

        <h2 style='color:#0b3d91;'>Thank You for Contacting Drive Time Learning</h2>

        <p>Dear {appointments.FullName},</p>

        <p>
            Thank you for reaching out to <strong>Drive Time Learning</strong>.
            We have successfully received your enquiry and a member of our team
            will contact you shortly.
        </p>

        <p>
            <strong>Your submitted details:</strong>
        </p>

        <ul>
            <li><strong>Phone:</strong> {appointments.PhoneNumber}</li>
            <li><strong>Email:</strong> {appointments.Email}</li>
            <li><strong>Preferred Contact Time:</strong> {appointments.Description}</li>
        </ul>

        <p>
            If any of the above information is incorrect, please reply to this
            email and let us know.
        </p>

        <p>
            Kind regards,<br/>
            <strong>Drive Time Learning</strong><br/>
            <a href='mailto:usman-majeed@drivetimelearning.co.uk'>
                Drive Time Learning Team
            </a>
        </p>

        <hr style='margin-top:30px;'/>

        <p style='font-size:12px; color:#777;'>
            This is an automated message sent on {DateTime.Now:f}.  
            Please do not reply unless you need to update your details.
        </p>

    </body>
    </html>"
                    };

                    // CLIENT EMAIL LOGGER
                    _logger.LogInformation(
                        "Sending client confirmation email to {Email}",
                        appointments.Email);

                    // SEND TO CLIENT
                    await _emailService.SendEmailAsync(
                        clientEmail.To,
                        clientEmail.Subject,
                        clientEmail.Body
                    );
                    
                    appointments.Agreement = true;
                    _schoolDbContext.Appointment.Add(appointments);
                    await _schoolDbContext.SaveChangesAsync();
                    _logger.LogInformation(
                        "Appointment saved successfully for {Email}",
                            appointments.Email);
                    TempData["SuccessMessage"] = "Thank you! Your request has been submitted successfully. We will contact you shortly.";
                    
                }
                catch (Exception ex)
                {
                    _logger.LogError(
            ex,
            "Error processing appointment for {Email}",
            appointments?.Email);

                    ViewBag.Error = "Some Error: " + ex.Message;
                }
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogWarning(
            "Invalid appointment submission for {Email}",
            appointments?.Email);
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
