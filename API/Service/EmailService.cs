using System.Net;
using System.Net.Mail;
using API.Interface;
using Microsoft.Extensions.Logging;

namespace API.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var client = new SmtpClient("live.smtp.mailtrap.io", 587)
                {
                    Credentials = new NetworkCredential("api", "876b3560cfd9ce765b1a3880c7f991e7"),
                    EnableSsl = true
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("mailtrap@demomailtrap.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(to);

                await client.SendMailAsync(mailMessage);
                System.Console.WriteLine("Sent");

                _logger.LogInformation($"Email sent successfully to {to}");
            }

            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while sending email: {ex.Message}");
                throw;
            }
        }
    }
}
