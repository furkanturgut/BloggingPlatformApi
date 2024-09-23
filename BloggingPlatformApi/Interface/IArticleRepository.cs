using BloggingPlatformApi.Models;

namespace BloggingPlatformApi.Interface
{
    public interface IArticleRepository
    {
        ICollection<Article> GetArticles();
        Article GetArticle(int ArticleId);
        ICollection<Tag> GetArticleTags (int ArticleId);
        bool ArticleExist (int ArticleId);
    }
}
