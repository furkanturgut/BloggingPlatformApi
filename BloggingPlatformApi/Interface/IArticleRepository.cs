using BloggingPlatformApi.Models;

namespace BloggingPlatformApi.Interface
{
    public interface IArticleRepository
    {
        ICollection<Article> GetArticles();
        Article GetArticle(int ArticleId);
        ICollection<Tag> GetArticleTags (int ArticleId);
        Writer GetArticleWriter (int ArticleId);
        bool CreateArticle (Article article);
        bool Save();
        bool ArticleExist (int ArticleId);
    }
}
