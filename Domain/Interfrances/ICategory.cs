using Domain.Models;

namespace Domain.Interfrances;

public interface ICategory
{
    void AddCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(int id);
    Category GetCategory(int id);
    List<Category> GetCategories();
}
