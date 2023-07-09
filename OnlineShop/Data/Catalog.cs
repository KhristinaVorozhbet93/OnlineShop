﻿using OnlineShop.Models;

namespace OnlineShop.Data
{
    public class Catalog
    {
        private readonly List<Product> _products;

        public Catalog()
        {
            _products = GenerateProducts(10);
        }
        public List<Product> GetProducts()
        {
            return _products;
            throw new ArgumentNullException(nameof(_products));
        }
        public void AddProduct(Product product)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(product));
            _products.Add(product);
        }
        private static List<Product> GenerateProducts(int count)
        {
            var random = new Random();
            var products = new Product[count];

            var productNames = new string[]
            {
            "Молоко",
            "Хлеб",
            "Яблоки",
            "Макароны",
            "Сахар",
            "Кофе",
            "Чай",
            "Рис",
            "Масло подсолнечное",
            "Сыр"
            };

            for (int i = 0; i < count; i++)
            {
                var name = productNames[i];
                var price = random.Next(50, 500);
                var producedAt = DateTime.Now.AddDays(-random.Next(1, 30));
                var expiredAt = producedAt.AddDays(random.Next(1, 365));


                products[i] = new Product(name, price)
                {
                    Id = Guid.NewGuid(),
                    Description = "Описание " + name,
                    ProducedAt = producedAt,
                    ExpiredAt = expiredAt
                };
            }
            return products.ToList();
        }
    }
}
