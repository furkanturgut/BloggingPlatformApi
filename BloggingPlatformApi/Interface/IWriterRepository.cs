using BloggingPlatformApi.Models;

namespace BloggingPlatformApi.Interface
{
    public interface IWriterRepository
    {
        ICollection<Writer> GetWriters();
        Writer GetWriter(int WriterId);
        ICollection<Article> GetArticleByWriter (int WriterId);
        Writer GetArticleWriter(int ArticleId); 
        bool WriterExists(int WriterId);    
    }
}
