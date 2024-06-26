using Slush.Data.Entity;

namespace Slush.Repositories.IRepository
{
    public interface ICategoriesByUserRepository
    {
        Task<List<CategoryByUser>> GetAllCategoriesByUser();
        Task<CategoryByUser> UpdateCategoriesByUser(CategoryByUser category);
        Task Add(CategoryByUser category);
        Task DeleteCategoryByUser(Guid id);
        Task<CategoryByUser?> GetById(Guid id);
        Task<List<CategoryByUser?>> GetAllById(List<Guid> ids);
    }
}
