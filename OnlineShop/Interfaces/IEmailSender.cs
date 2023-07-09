namespace OnlineShop.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string recepientEmail, string subject, string message);
    }
}
