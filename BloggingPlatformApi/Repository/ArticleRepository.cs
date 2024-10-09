using AutoMapper;
using BloggingPlatformApi.Data;
using BloggingPlatformApi.Interface;
using BloggingPlatformApi.Models;
using Microsoft.EntityFrameworkCore;

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

        public bool CreateArticle(Article article)
        {
            _dataContext.Add(article);
            return Save();
        }

        public bool DeleteArticle(Article article)
        {
            _dataContext.Remove(article);
            return Save();
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

    

        public Writer GetArticleWriter(int ArticleId)
        {
            return _dataContext.Articles.Where(a=> a.Id == ArticleId).Select(w => w.Writer).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true: false;
        }

        public bool UpdateArticle(Article article)
        {
            _dataContext.Update(article);
            return Save();
        }
    }
}
