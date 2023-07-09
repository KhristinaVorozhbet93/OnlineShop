using OnlineShop.Interfaces;
using System.Diagnostics;

namespace OnlineShop.Services
{
    public class SalesNotificatorBackgroundService : BackgroundService
    {
        private readonly ILogger<SalesNotificatorBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public SalesNotificatorBackgroundService(ILogger<SalesNotificatorBackgroundService> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await using var scope = _serviceProvider.CreateAsyncScope();
                var localServiceProvider = scope.ServiceProvider;
                var emailSender = localServiceProvider.GetRequiredService<IEmailSender>();

                var users = new User[]
                {
                new ("ptykhina.khristi@mail.ru")
                };

                var sw = Stopwatch.StartNew();
                foreach (var user in users)
                {
                    await emailSender.SendEmailAsync(user.email, "Промоакции", "Список акций");
                    _logger.LogInformation($"Email send to {user.email} in {sw.ElapsedMilliseconds} ms");
                }
                await Task.Delay(TimeSpan.FromDays(1)); 
            }
        }
    }

    public record User(string email);
}
