using BloggingPlatformApi.Data;
using BloggingPlatformApi.Interface;
using BloggingPlatformApi.Models;

namespace BloggingPlatformApi.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _dataContext;

        public CategoryRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public bool CategoryExist(int id)
        {
            return _dataContext.Categories.Any(c => c.Id == id);
        }

        public ICollection<Article> GetArticleByCategory(int categoryId)
        {
            return _dataContext.Articles.Where(c => c.Category.Id == categoryId).OrderBy(c=> c.Id).ToList();
            
        }

        public ICollection<Category> GetCategories()
        {
            return _dataContext.Categories.OrderBy(p=> p.Id).ToList();
        }

        public Category GetArticleCategory(int ArticleId)
        {
            return _dataContext.Articles.Where(a => a.Id == ArticleId).Select(c => c.Category).FirstOrDefault();

        }

        public Category GetCategory(int id)
        {
            return _dataContext.Categories.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool CreateCategory(Category category)
        {
            _dataContext.Add(category);
            return Save();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _dataContext.Update(category);
            return Save();
        }
    }
}
