using MailKit.Net.Smtp;
using MimeKit;
using System.Net;

namespace OnlineShop.Services
{
    public class MailKitSmptEmailSender
    {
        public async Task SendEmailAsync(string recepientEmail, string subject, string message)
        {
            ArgumentNullException.ThrowIfNull(recepientEmail);
            ArgumentNullException.ThrowIfNull(subject);
            ArgumentNullException.ThrowIfNull(message);

            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Академия TOP", "asp2023pv112@rodion-m.ru"));
            emailMessage.To.Add(new MailboxAddress("", recepientEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.beget.com", 25, false);
                await client.AuthenticateAsync("asp2023pv112@rodion-m.ru", "");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
