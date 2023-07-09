using OnlineShop.Models;

namespace OnlineShop.Interfaces
{
    public interface ICatalog
    {
        public interface ICatalog
        {
            List<Product> GetProducts();
            Product GetProductById(Guid id);
            void AddProduct(Product product);
            void DeleteProduct(Guid productId);
            void UpdateProduct(Guid productId, Product newProduct);
            void ClearCatalog();

        }
    }
}
