using Slush.Entity.Profile;

namespace Slush.Repositories.IRepository
{
    public interface IFriendsRepository
    {
        Task<List<Friends>> GetAllFriends();
        Task<Friends> UpdateFriends(Friends friends);
        Task Add(Friends friend);
        Task DeleteFriends(Guid id);
        Task<Friends?> GetById(Guid id);
        Task<List<Friends?>> GetByUserId(Guid id);
        Task<List<Friends?>> GetByUserIds(List<Guid> id);
    }
}
