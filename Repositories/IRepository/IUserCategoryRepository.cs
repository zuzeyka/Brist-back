using Slush.Entity.Profile;

namespace Slush.Repositories.IRepository
{
    public interface IUserCategoryRepository
    {
        Task<List<UserCategory>> GetAllUserCategories();
        Task<List<UserCategory>> GetAllCategoriesByUser(Guid id);
        Task<UserCategory> UpdateUserCategory(UserCategory userCategory);
        Task Add(UserCategory userCategory);
        Task Delete(Guid id);
        Task<UserCategory?> GetById(Guid id);
        Task<List<UserCategory?>> GetByIds(List<Guid> ids);
    }
}
