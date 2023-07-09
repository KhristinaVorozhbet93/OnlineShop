using OnlineShop.Models;

namespace OnlineShop.Data
{
    public class Catalog
    {
        private readonly List<Product> _products;
        private object _productsSyncObj = new();

        public Catalog()
        {
            _products = GenerateProducts(10);
        }
        public List<Product> GetProducts()
        {
            lock (_productsSyncObj)
            {
                return _products;
            }     
            throw new ArgumentNullException(nameof(_products));
        }
        public Product GetProductById(Guid id)
        {
            for (int i = 0; i < _products.Count; i++)
            {
                if (_products[i].Id == id)
                {
                    lock (_productsSyncObj)
                    {
                        return _products[i];
                    }
                }
            }
            throw new ArgumentException($"Продукта с ID={id} не существует!");
        }
        public void AddProduct(Product product)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(product));
            lock (_productsSyncObj)
            {
                _products.Add(product);
            }
        }
        public void UpdateProduct(Guid productId, Product newProduct)
        {
            ArgumentException.ThrowIfNullOrEmpty(nameof(newProduct));
            foreach (var product in _products)
            {
                if (product.Id == productId)
                {
                    product.Name = newProduct.Name;
                    product.Price = newProduct.Price;
                    product.ExpiredAt = newProduct.ExpiredAt;
                    product.ProducedAt = newProduct.ProducedAt;
                    product.Description = newProduct.Description;
                }
                else
                {
                    throw new ArgumentException($"Продукта с ID={productId} не существует!");
                }
            }
        }
        public void DeleteProduct(Guid productId)
        {
            foreach (var product in _products)
            {
                if (product.Id == productId)
                {
                    _products.Remove(product);
                }
            }
            ArgumentException.ThrowIfNullOrEmpty($"Продукта с ID={productId} не существует!");
        }
        public void ClearCatalog()
        {
            _products.Clear();
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
