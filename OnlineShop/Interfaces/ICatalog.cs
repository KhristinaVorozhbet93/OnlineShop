using OnlineShop.Models;

namespace OnlineShop.Interfaces
{
    public interface ICatalog
    {
        List<Product> GetProducts(IClock clock);
        Product GetProductById(Guid id, IClock clock);
        void AddProduct(Product product);
        void DeleteProduct(Guid productId);
        void UpdateProduct(Guid productId, Product newProduct);
        void ClearCatalog();

    }

}
