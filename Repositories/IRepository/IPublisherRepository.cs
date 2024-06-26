using Slush.Entity.Store.Product.Creators;

namespace Slush.Repositories.IRepository
{
    public interface IPublisherRepository
    {
        Task<List<Publisher>> GetAllPublishers();
        Task<Publisher> UpdatePublisher(Publisher publisher);
        Task Add(Publisher publisher);
        Task DeletePublisher(Guid id);
        Task<Publisher?> GetById(Guid id);
        Task<List<Publisher?>> GetByIds(List<Guid> ids);
    }
}
