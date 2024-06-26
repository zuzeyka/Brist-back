using Slush.Data.Entity;

namespace Slush.Repositories.IRepository
{
    public interface ICategoriesByAuthorRepository
    {
        Task<List<CategoryByAuthor>> GetAllCategoriesByAuthor();
        Task<CategoryByAuthor> UpdateCategoriesByAuthor(CategoryByAuthor category);
        Task Add(CategoryByAuthor category);
        Task DeleteCategoryByAuthor(Guid id);
        Task<CategoryByAuthor?> GetById(Guid id);
        Task<List<CategoryByAuthor>> GetAllCategoriesByIds(List<Guid> guidList);
    }
}
