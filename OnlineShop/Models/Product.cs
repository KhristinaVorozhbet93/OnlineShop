namespace OnlineShop.Models
{
    public class Product
    {
        public Product(string name, decimal price)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(name));
            Name = name;
            Price = price;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ProducedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
