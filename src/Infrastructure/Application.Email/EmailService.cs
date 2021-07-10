using System;
using System.Net.Mail;
using Application.Domain.Emails;

namespace Application.Email
{
    public class EmailService : IEmailService
    {
        public void Send(IEmail email)
        {
            Console.WriteLine("Pretending to send email until SMTP server configured.");
            
            //Project: Uncomment this code once you have an smtp server configured.
            //var mailMessage = ConvertToMailMessage(email);
            //using var smtp = new SmtpClient();
            //smtp.Send(mailMessage);
        }

        private MailMessage ConvertToMailMessage(IEmail email)
        {
            var mailMessage = new MailMessage
            {
                Subject = email.Subject,
                Body = email.Body(),
                IsBodyHtml = email.IsHtml
            };
            
            mailMessage.To.Add(email.Recipient.EmailAddress);

            return mailMessage;
        }
    }
}