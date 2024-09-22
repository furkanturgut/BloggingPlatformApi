using BloggingPlatformApi.Models;

namespace BloggingPlatformApi.Interface
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        ICollection<Article> GetArticleByCategory (int categoryId);
        Category GetCategory(int id);
        bool CategoryExist(int id);
    }
}
