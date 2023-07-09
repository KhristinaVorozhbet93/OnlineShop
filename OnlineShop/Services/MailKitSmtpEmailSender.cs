using MailKit.Net.Smtp;
using MimeKit;
using OnlineShop.Interfaces;
using System.Net;

namespace OnlineShop.Services
{
    public class MailKitSmptEmailSender : IEmailSender, IAsyncDisposable
    {
        private readonly SmtpClient _smtpClient = new SmtpClient();

        public async ValueTask DisposeAsync()
        {
            await _smtpClient.DisconnectAsync(true);
        }
        private async Task EnsureConnectAndAuthenticateAsync()
        {
            if (!_smtpClient.IsConnected)
            {
                await _smtpClient.ConnectAsync("smtp.beget.com", 25, false);
            }
            if (!_smtpClient.IsAuthenticated)
            {
                await _smtpClient.AuthenticateAsync("asp2023pv112@rodion-m.ru", "");
            }
        }
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
            await EnsureConnectAndAuthenticateAsync();
            await _smtpClient.SendAsync(emailMessage);
        }
    }
}
