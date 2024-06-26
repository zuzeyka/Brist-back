using Slush.Data.Entity.Community;

namespace Slush.Repositories.IRepository
{
    public interface IGroupRepository
    {
        Task<List<Group>> GetAllGroups();
        Task<Group> UpdateGroup(Group group);
        Task Add(Group group);
        Task DeleteGroup(Guid id);
        Task<Group?> GetById(Guid id);
        Task<List<Group?>> GetByIds(List<Guid> id);
    }
}
