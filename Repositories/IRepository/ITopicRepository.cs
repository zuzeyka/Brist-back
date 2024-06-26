using Slush.Data.Entity.Community;

namespace Slush.Repositories.IRepository
{
    public interface ITopicRepository
    {
        Task<List<Topic>> GetAllTopics();
        Task<Topic> UpdateTopic(Topic topic);
        Task Add(Topic topic);
        Task DeleteTopic(Guid id);
        Task<Topic?> GetById(Guid id);
        Task<List<Topic?>> GetByAttachedId(Guid id);
        Task<List<Topic?>> GetByIds(List<Guid> ids);
    }
}
