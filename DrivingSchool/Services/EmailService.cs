using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using DrivingSchool.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace DrivingSchool.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var senderEmail = new MailAddress(_emailSettings.Email ?? "Sender Email Not Found", "Drive Time Learning");
                var receiverEmail = new MailAddress(to, "Receiver");
                var password = _emailSettings.Password;
                var smtp = new SmtpClient
                {
                    Host = _emailSettings.Host ?? "Host Not Found",
                    Port = _emailSettings.Port,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {

                    mess.To.Add(receiverEmail);
                    mess.CC.Add("drive.time.learning@outlook.com"); // Add your own email to CC
                    await smtp.SendMailAsync(mess);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Email sending failed.", ex);
            }
        }
    }
}
