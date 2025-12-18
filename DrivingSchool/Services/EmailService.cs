using DrivingSchool.Models;
using DrivingSchool.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailService> _logger;

    public EmailService(
        IOptions<EmailSettings> emailSettings,
        ILogger<EmailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        try
        {
            var senderEmail = new MailAddress(_emailSettings.Email);
            var receiverEmail = new MailAddress(to);

            using var smtp = new SmtpClient
            {
                Host = _emailSettings.Host,
                Port = _emailSettings.Port,
                EnableSsl = true,
                Credentials = new NetworkCredential(senderEmail.Address, _emailSettings.Password)
            };

            using var message = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.CC.Add("drive.time.learning@outlook.com");

            await smtp.SendMailAsync(message);

            _logger.LogInformation("Email sent successfully to {Recipient}", to);
        }
        catch (SmtpException ex)
        {
            _logger.LogError(ex, "SMTP error while sending email to {Recipient}", to);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while sending email to {Recipient}", to);
            throw;
        }
    }
}