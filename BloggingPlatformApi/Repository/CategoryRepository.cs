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

        public ICollection<Category> GetCategories()
        {
            return _dataContext.Categories.OrderBy(p=> p.Id).ToList();
        }

        public Category GetCategory(int id)
        {
            return _dataContext.Categories.Where(p => p.Id == id).FirstOrDefault();
        }
    }
}
