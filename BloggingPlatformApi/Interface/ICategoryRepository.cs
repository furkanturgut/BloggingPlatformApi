using BloggingPlatformApi.Models;

namespace BloggingPlatformApi.Interface
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        bool CategoryExist(int id);
    }
}
