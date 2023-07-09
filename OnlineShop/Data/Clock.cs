using OnlineShop.Interfaces;

namespace OnlineShop.Data
{
    public class Clock : IClock
    {
        public DateTime GetDateTimeUtc()
        {
            return DateTime.UtcNow;
        }
    }
}
