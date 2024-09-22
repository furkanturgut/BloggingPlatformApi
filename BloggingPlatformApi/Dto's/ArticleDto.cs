using BloggingPlatformApi.Models;

namespace BloggingPlatformApi.Dto_s
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public Writer Writer { get; set; }
        public Category Category { get; set; }
    }
}
