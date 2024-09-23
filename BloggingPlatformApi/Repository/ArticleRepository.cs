using AutoMapper;
using BloggingPlatformApi.Data;
using BloggingPlatformApi.Interface;
using BloggingPlatformApi.Models;

namespace BloggingPlatformApi.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly DataContext _dataContext;

        public ArticleRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        public bool ArticleExist(int ArticleId)
        {
            return _dataContext.Articles.Any(a=> a.Id == ArticleId);
        }

        public Article GetArticle(int ArticleId)
        {
            return _dataContext.Articles.FirstOrDefault(a => a.Id == ArticleId);
        }


        public ICollection<Article> GetArticles()
        {
            return _dataContext.Articles.OrderBy(a => a.Id).ToList();
        }

        public ICollection<Tag> GetArticleTags(int ArticleId)
        {
           return _dataContext.ArticleTags.Where(at => at.ArticleId == ArticleId).Select(a=> a.Tag).ToList();
        }

        

        

    }
}
