using ApplicationLayer.Services.IMailSendService;
using System.Net.Mail;
using System.Net;
using static Shared.Constants.UserConstants;

namespace InfrastructureLayer.MailSend
{
    public class MailSenderRepository : IMailSender
    {
        public void SendMailToUser(string email)
        {
            string SenderMail = adminMail;
            string password = mailPassword;

            MailMessage message = new()
            {
                From = new MailAddress(SenderMail),
                Subject = "Security Alert!",
            };
            message.To.Add(email);

            message.Body = mailBody.Replace("{UserEmail}", email);

            message.IsBodyHtml = true;

            var client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(SenderMail, password),
                EnableSsl = true
            };
            client.Send(message);
        }
    }
}
