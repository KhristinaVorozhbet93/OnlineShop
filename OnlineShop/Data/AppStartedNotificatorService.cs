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
                var memory = 0.0;
                using (Process proc = Process.GetCurrentProcess())
                {
                    memory = proc.PrivateMemorySize64 / (1024 * 1024);
                }
                await _emailSender.SendEmailAsync("ptykhina.khristi@mail.ru", "Тест приложения", $"Использовано памяти: {memory} мегабайт");
                await Task.Delay(TimeSpan.FromMinutes(1));
            }

        }
    }
}
