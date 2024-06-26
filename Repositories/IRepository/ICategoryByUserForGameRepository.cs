using Slush.Entity.Profile;

namespace Slush.Repositories.IRepository
{
    public interface ICategoryByUserForGameRepository
    {
        Task<List<CategoryByUserForGame>> GetAllCategoryByUserForGames();
        Task<CategoryByUserForGame> UpdateCategoryByUserForGame(CategoryByUserForGame categoryByUserForGame);
        Task Add(CategoryByUserForGame newCategoryByUserForGame);
        Task Delete(Guid id);
        Task<CategoryByUserForGame?> GetById(Guid id);
        Task<List<CategoryByUserForGame?>> GetByIds(List<Guid> ids);
    }
}
