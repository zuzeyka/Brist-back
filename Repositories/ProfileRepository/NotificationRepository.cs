using Slush.Data;
using Slush.Entity.Profile;
using Microsoft.EntityFrameworkCore;
using Slush.Repositories.IRepository;

namespace Slush.Repositories.ProfileRepository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DataContext _context;

        public NotificationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Notification?> GetById(Guid id)
        {
            return await _context.dbNotifications
                .Where(n => n.id == id)
                .Where(n => n.deletedAt == null)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Notification>> GetByUserId(Guid userId)
        {
            return await _context.dbNotifications
                .Where(n => n.userId == userId)
                .Where(n => n.deletedAt == null)
                .OrderByDescending(n => n.createdAt)
                .ToListAsync();
        }

        public async Task Add(Notification notification)
        {
            await _context.dbNotifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        public async Task<Notification?> MarkRead(Guid id)
        {
            var existing = await _context.dbNotifications.FindAsync(id);
            if (existing != null)
            {
                existing.isRead = true;
                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task MarkAllRead(Guid userId)
        {
            await _context.dbNotifications
                .Where(n => n.userId == userId && n.deletedAt == null && !n.isRead)
                .ExecuteUpdateAsync(set => set.SetProperty(n => n.isRead, true));
        }

        public async Task ClearAll(Guid userId)
        {
            await _context.dbNotifications
                .Where(n => n.userId == userId && n.deletedAt == null)
                .ExecuteUpdateAsync(set => set.SetProperty(n => n.deletedAt, DateTime.Now));
        }
    }
}
