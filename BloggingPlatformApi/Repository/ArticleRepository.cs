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
        public bool CategoryExist(int ArticleId)
        {
            return _dataContext.Articles.Any(a=> a.Id == ArticleId);
        }

        public Article GetArticle(int ArticleId)
        {
            return _dataContext.Articles.FirstOrDefault(a => a.Id == ArticleId);
        }

        public Category GetArticleCategory(int ArticleId)
        {
            return _dataContext.Articles.Where(a => a.Id == ArticleId).Select(a => a.Category).FirstOrDefault();
        }

        public ICollection<Article> GetArticles()
        {
            return _dataContext.Articles.OrderBy(a => a.Id).ToList();
        }

        public Writer GetArticleWriter(int ArticleId)
        {
            return _dataContext.Articles.Where(a => a.Id == ArticleId).Select(a => a.Writer).FirstOrDefault();
        }
    }
}
