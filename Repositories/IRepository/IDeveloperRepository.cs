using Slush.Entity.Store.Product.Creators;

namespace Slush.Repositories.IRepository
{
    public interface IDeveloperRepository
    {
        Task<List<Developer>> GetAllDevelopersRepositories();
        Task<Developer> UpdateDeveloper(Developer developer);
        Task Add(Developer developer);
        Task DeleteDeveloper(Guid id);
        Task<Developer?> GetById(Guid id);
        Task<List<Developer>> GetByIds(List<Guid> ids);
    }
}
