using Domain.Interfrances;
using Domain.Models;
using Infrastructure.Data;
using Npgsql;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategory
{
    public void AddCategory(Category category)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "insert into category(name) values(@name)";
        var cmd = new NpgsqlCommand(query,conn);
        cmd.Parameters.AddWithValue("name",category.Name);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Added!"); 
    }

    public void DeleteCategory(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "delete from category where id = @id";
        var cmd = new NpgsqlCommand(query,conn);
        cmd.Parameters.AddWithValue("id",id);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Deleted!");
    }

    public List<Category> GetCategories()
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select * from category";
        var cmd = new NpgsqlCommand(query,conn);
        var data = cmd.ExecuteReader();
        var categories = new List<Category>();
        while (data.Read())
        {
            var category = new Category();
            category.Id = data.GetInt32(0);
            category.Name = data.GetString(1);
            categories.Add(category);
        }
        return categories;
    }

    public Category GetCategory(int id)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "select * from category where id = @id";
        var cmd = new NpgsqlCommand(query,conn);
        cmd.Parameters.AddWithValue("id",id);
        var data = cmd.ExecuteReader();
        var category = new Category();
        while (data.Read())
        {
            category.Id = data.GetInt32(0);
            category.Name = data.GetString(1);
        }
        return category;
    }

    public void UpdateCategory(Category category)
    {
        using var conn = Database.Connection();
        conn.Open();
        var query = "update category set name = @name where id = @id";
        var cmd = new NpgsqlCommand(query,conn);
        cmd.Parameters.AddWithValue("name",category.Name);
        cmd.Parameters.AddWithValue("id",category.Id);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Updeted!");
    }

}
