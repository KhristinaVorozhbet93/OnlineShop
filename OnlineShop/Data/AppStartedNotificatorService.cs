using OnlineShop.Interfaces;
using System.Diagnostics;

namespace OnlineShop.Data
{
    public class AppStartedNotificatorService : BackgroundService
    {
        private readonly IEmailSender _emailSender;
        public AppStartedNotificatorService(IEmailSender emailSender)
        {
            _emailSender = emailSender ?? throw new ArgumentException(nameof(emailSender));
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _emailSender.SendEmailAsync("ptykhina.khristi@mail.ru", "Тест приложения", "Приложение запущено");
                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }
    }
}
