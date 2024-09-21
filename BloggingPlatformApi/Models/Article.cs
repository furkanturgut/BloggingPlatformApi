using System.ComponentModel;

namespace BloggingPlatformApi.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public Writer Writer {  get; set; }
        public Category Category { get; set; }

        public ICollection<ArticleTag> ArticleTags { get; set; }
    }
}
