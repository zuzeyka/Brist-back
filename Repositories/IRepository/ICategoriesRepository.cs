using Slush.Data.Entity;

namespace Slush.Repositories.IRepository
{
    public interface ICategoriesRepository
    {
        Task<List<Categories>> GetAll();
        Task UpdateCategories(Categories category);
        Task Add(Categories category);
        Task DeleteCategories(Guid id);
        Task<Categories?> GetById(Guid id);
        Task<List<Categories?>> GetByIds(List<Guid> id);
    }
}
