using Slush.Entity;

namespace Slush.Repositories.IRepository
{
    public interface IDiscussionRepository
    {
        Task<List<Discussion>> GetAllDiscussions();
        Task<Discussion> UpdateDiscussion(Discussion discussion);
        Task Add(Discussion discussion);
        Task DeleteDiscussion(Guid id);
        Task<Discussion?> GetById(Guid id);
        Task<List<Discussion?>> GetByAttachedId(Guid id);
        Task<List<Discussion?>> GetByIds(List<Guid> id);
    }
}
