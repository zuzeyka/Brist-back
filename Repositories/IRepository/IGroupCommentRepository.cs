using Slush.Data.Entity.Community;

namespace Slush.Repositories.IRepository
{
    public interface IGroupCommentRepository
    {
        Task<List<GroupComment>> GetAllGroupComments();
        Task<GroupComment> UpdateGroupComment(GroupComment comment);
        Task Add(GroupComment comment);
        Task DeleteGroupComment(Guid id);
        Task<GroupComment?> GetById(Guid id);
        Task<List<GroupComment?>> GetByIds(List<Guid> id);
    }
}
