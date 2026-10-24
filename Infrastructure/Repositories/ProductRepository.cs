using Domain.Interfrances;
using Domain.Models;
using Infrastructure.Data;
using Npgsql;

namespace Infrastructure.Repositories;

public class ProductRepository : IProduct
{
    public void AddProduct(Product product)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "insert into product(name,price,category_id) values(@name, @price, @id)";
        var cmd = new NpgsqlCommand(query,conn);
        cmd.Parameters.AddWithValue("name",product.Name);
        cmd.Parameters.AddWithValue("price",product.Price);
        cmd.Parameters.AddWithValue("id",product.CategpryId);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Added!");
    }

    public void DeleteProduct(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "delete from product where id = @id";
        var cmd = new NpgsqlCommand(query,conn);
        cmd.Parameters.AddWithValue("id",id);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Deleted!");
    }

    public Product GetProduct(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select * from product where id = @id";
        var cmd = new NpgsqlCommand(query,conn);
        cmd.Parameters.AddWithValue("id",id);
        var data = cmd.ExecuteReader();
        var product = new Product();
        while (data.Read())
        {
            product.Id = data.GetInt32(0);
            product.Name = data.GetString(1);
            product.Price = data.GetDecimal(2);
            product.CategpryId = data.GetInt32(3);
        }
        return product;
    }

    public List<Product> GetProductByCategory(int a)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = $"select product.name, category.name from product join category on product.category_id = category.id where category.id = @id";
        var cmd = new NpgsqlCommand(query, conn);
        cmd.Parameters.AddWithValue("id", a);
        var data = cmd.ExecuteReader();
        var products = new List<Product>();
        while (data.Read())
        {
            var product = new Product();
            var category = new Category();
            product.Name = data.GetString(0);
            System.Console.WriteLine($"Product:{data.GetString(0)}, Category:{data.GetString(1)}");
            products.Add(product);
        }
        return products;
    }

    public List<Product> GetProducts()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select * from product";
        var cmd = new NpgsqlCommand(query,conn);
        var data = cmd.ExecuteReader();
        var products = new List<Product>();
        while (data.Read())
        {
            var product = new Product();
            product.Id = data.GetInt32(0);
            product.Name = data.GetString(1);
            product.Price = data.GetDecimal(2);
            product.CategpryId = data.GetInt32(3);
            products.Add(product);
        }
        return products;
    }

    public void UpdateProduct(Product product)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "update product set name = @name, price = @price, category_id = @o where id = @id";
        var cmd = new NpgsqlCommand(query,conn);
        cmd.Parameters.AddWithValue("name",product.Name);
        cmd.Parameters.AddWithValue("price",product.Price);
        cmd.Parameters.AddWithValue("o",product.CategpryId);
        cmd.Parameters.AddWithValue("id",product.Id);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Updeted!");
    }

}
