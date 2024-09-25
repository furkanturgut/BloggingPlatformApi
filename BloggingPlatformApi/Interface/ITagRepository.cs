using BloggingPlatformApi.Models;

namespace BloggingPlatformApi.Interface
{
    public interface ITagRepository
    {
        ICollection<Tag> GetTags();
        Tag GetTag(int TagId);
        bool TagExist (int TagId);
        ICollection<Article> GetArticleByTag(int TagId);

    }
}
