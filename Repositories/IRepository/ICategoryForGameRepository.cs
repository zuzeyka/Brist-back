using Slush.Data.Entity;

namespace Slush.Repositories.IRepository
{
    public interface ICategoryForGameRepository
    {
        Task<List<CategoryForGame>> GetAll();
        Task<CategoryForGame> UpdateCategoryForGame(CategoryForGame category);
        Task Add(CategoryForGame category);
        Task DeleteCategoryForGame(Guid id);
        Task<CategoryForGame?> GetById(Guid id);
        Task<List<CategoryForGame?>> GetByGameId(Guid id);
        Task<List<CategoryForGame?>> GetByGameIds(List<Guid> id);
    }
}
