using Slush.Entity.Profile;

namespace Slush.Repositories.IRepository
{
    public interface INotificationRepository
    {
        Task<Notification?> GetById(Guid id);
        Task<List<Notification>> GetByUserId(Guid userId);
        Task Add(Notification notification);
        Task<Notification?> MarkRead(Guid id);
        Task MarkAllRead(Guid userId);
        Task ClearAll(Guid userId);
    }
}
