using OnlineShop.Interfaces;
using System.Diagnostics;

namespace OnlineShop.Services
{
    public class SalesNotificatorBackgroundService : BackgroundService
    {
        private readonly IEmailSender _emailSender;
        private readonly ILogger<SalesNotificatorBackgroundService> _logger;
        public SalesNotificatorBackgroundService(IEmailSender emailSender, 
            ILogger<SalesNotificatorBackgroundService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _emailSender = emailSender ?? throw new ArgumentException(nameof(emailSender));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var users = new User[]
            {
                new ("ptykhina.khristi@mail.ru")
            };

            var sw = Stopwatch.StartNew();
            foreach (var user in users)
            {
                await _emailSender.SendEmailAsync(user.email, "Промоакции", "Список акций");
                _logger.LogInformation($"Email send to {user.email} in {sw.ElapsedMilliseconds} ms");
            }
                
        }
    }

    public record User(string email);
}
