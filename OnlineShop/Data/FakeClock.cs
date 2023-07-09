using OnlineShop.Interfaces;

namespace OnlineShop.Data
{
    public class FakeClock : IClock
    {
        private DateTime _dateUtc; 
        public FakeClock(DateTime dateUtc)
        {
            _dateUtc = dateUtc;
        }
        public DateTime GetDateTimeUtc() => _dateUtc;
    }
}
