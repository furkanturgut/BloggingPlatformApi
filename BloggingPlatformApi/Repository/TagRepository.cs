using BloggingPlatformApi.Data;
using BloggingPlatformApi.Interface;
using BloggingPlatformApi.Models;

namespace BloggingPlatformApi.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly DataContext _context;

        public TagRepository(DataContext context)
        {
            this._context = context;
        }

        public bool CreateTag(int ArticleId, Tag tag)
        {
            var article = _context.Articles.FirstOrDefault(a => a.Id == ArticleId);
            if (article != null)
            {
                var ArticleTags = new ArticleTag()
                {
                    Article = article,
                    Tag = tag
                };
                _context.Add(ArticleTags);
            }
            _context.Add(tag);
            return Save();
        }

        public ICollection<Article> GetArticleByTag(int TagId)
        {
            return _context.ArticleTags.Where(t=> t.TagId == TagId).Select(t=>t.Article).ToList();
        }

        public Tag GetTag(int TagId)
        {
            return _context.Tags.FirstOrDefault(t => t.Id == TagId);
        }

        public ICollection<Tag> GetTags()
        {
            return _context.Tags.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool TagExist(int TagId)
        {
            return _context.Tags.Any(t => t.Id == TagId);
        }
    }
}
