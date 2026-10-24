using Domain.Models;

namespace Domain.Interfrances;

public interface IProduct
{
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(int id);
    Product GetProduct(int id);
    List<Product> GetProducts();
    List<Product> GetProductByCategory(int id);
}
