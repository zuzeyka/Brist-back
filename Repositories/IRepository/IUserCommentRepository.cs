using Slush.Data.Entity.Profile;

namespace Slush.Repositories.IRepository
{
    public interface IUserCommentRepository
    {
        Task<List<UserComment>> GetAllUserComments();
        Task<UserComment> UpdateUserComment(UserComment category);
        Task Add(UserComment comment);
        Task DeleteUserComment(Guid id);
        Task<UserComment?> GetById(Guid id);
        Task<List<UserComment?>> GetByUId(Guid id);
        Task<List<UserComment?>> GetByIds(List<Guid> ids);
    }
}
