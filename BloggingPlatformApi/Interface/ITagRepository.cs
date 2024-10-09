using BloggingPlatformApi.Models;

namespace BloggingPlatformApi.Interface
{
    public interface ITagRepository
    {
        ICollection<Tag> GetTags();
        Tag GetTag(int TagId);
        bool TagExist (int TagId);
        bool CreateTag (int ArticleId ,Tag tag);
        bool UpdateTag (Tag tag);
        bool DeleteTag (Tag tag);
        bool Save();
        ICollection<Article> GetArticleByTag(int TagId);
    }
}
