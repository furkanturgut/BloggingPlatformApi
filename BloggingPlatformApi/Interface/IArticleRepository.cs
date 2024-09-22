using BloggingPlatformApi.Models;

namespace BloggingPlatformApi.Interface
{
    public interface IArticleRepository
    {
        ICollection<Article> GetArticles();
        Article GetArticle(int ArticleId);
        Category GetArticleCategory (int ArticleId);
        Writer GetArticleWriter (int ArticleId);

        bool CategoryExist (int ArticleId);
    }
}
