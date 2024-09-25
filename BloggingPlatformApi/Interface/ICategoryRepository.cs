using BloggingPlatformApi.Models;

namespace BloggingPlatformApi.Interface
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        ICollection<Article> GetArticleByCategory (int categoryId);
        Category GetArticleCategory(int ArticleId);
        Category GetCategory(int id);
        bool CategoryExist(int id);
    }
}
