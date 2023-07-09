using OnlineShop.Interfaces;
using System.Diagnostics;

namespace OnlineShop.Data
{
    public class AppStartedNotificatorService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        public AppStartedNotificatorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var localServiceProvider = scope.ServiceProvider;
            var emailSender = localServiceProvider.GetRequiredService<IEmailSender>();

            while (!stoppingToken.IsCancellationRequested)
            {      
                var memory = 0.0;
                using (Process proc = Process.GetCurrentProcess())
                {
                    memory = proc.PrivateMemorySize64 / (1024 * 1024);
                }
                await emailSender.SendEmailAsync("ptykhina.khristi@mail.ru", "Тест приложения", $"Использовано памяти: {memory} мегабайт");
                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }
    }
}
