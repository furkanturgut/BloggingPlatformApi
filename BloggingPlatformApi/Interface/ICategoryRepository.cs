using BloggingPlatformApi.Models;

namespace BloggingPlatformApi.Interface
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        ICollection<Article> GetArticleByCategory (int categoryId);
        Category GetArticleCategory(int ArticleId);
        Category GetCategory(int id);
        bool CreateCategory (Category category);
        bool UpdateCategory (Category category);
        bool DeleteCategory (Category category);
        bool Save();
        bool CategoryExist(int id);
    }
}
