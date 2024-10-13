using System.Net.Mail;
using System.Net;

namespace Amazon.Services.Utilities.EmailSettings
{
	public class EmailService : IEmailService
    {
        public async Task SendEmail(Email email)
        {
            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587, 
                EnableSsl = true,
                Credentials = new NetworkCredential(
                "abdelrahmansaleh237@gmail.com", "qtsrrsewiuqezqek")
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("abdelrahmansaleh237@gmail.com"),
                Subject = email.Title,
                Body = email.Body,
                IsBodyHtml = true 
            };

            mailMessage.To.Add(email.To);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
