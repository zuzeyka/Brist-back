using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Entity.Profile;

namespace Slush.DAO.ProfileRepository
{
    public class SettingsRepository
    {
        private readonly DataContext _context;

        public SettingsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Settings>> GetAll()
        {
            return await _context.dbSettings
                .Where(s => s.deletedAt == null)
                .Select(s => new Settings()
                {
                    id = s.id,
                    attachedUserId = s.attachedUserId,
                    bigSaleNotification = s.bigSaleNotification,
                    saleFromWishlistNotification = s.saleFromWishlistNotification,
                    newCommentNotification = s.newCommentNotification,
                    friendRequestNotification = s.friendRequestNotification,
                    approvedFriendRequest = s.approvedFriendRequest,
                    declinedFriendRequest = s.declinedFriendRequest,
                    createdAt = s.createdAt
                }).ToListAsync();
        }

        public async Task<Settings> UpdateSettings(Settings newSettings)
        {
            var existing = await _context.dbSettings.FindAsync(newSettings.id);
            if(existing != null)
            {
                existing.bigSaleNotification = newSettings.bigSaleNotification;
                existing.saleFromWishlistNotification = newSettings.saleFromWishlistNotification;
                existing.newCommentNotification = newSettings.newCommentNotification;
                existing.friendRequestNotification = newSettings.friendRequestNotification;
                existing.approvedFriendRequest = newSettings.approvedFriendRequest;
                existing.declinedFriendRequest = newSettings.declinedFriendRequest;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task DeleteSettings(Guid id)
        {
            var existing = await _context.dbSettings.FindAsync(id);
            if(existing != null )
            {
                existing.deletedAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Add(Settings settings)
        {
            await _context.dbSettings.AddAsync(settings);
            await _context.SaveChangesAsync();
        }

        public async Task<Settings?> GetById(Guid id)
        {
            var response = await _context.dbSettings
                .Where(x => x.id == id)
                .Select(s => new Settings
                { 
                    id = s.id ,
                    bigSaleNotification = s.bigSaleNotification ,
                    saleFromWishlistNotification = s.saleFromWishlistNotification ,
                    newCommentNotification = s.newCommentNotification ,
                    friendRequestNotification = s.friendRequestNotification ,
                    approvedFriendRequest = s.approvedFriendRequest ,
                    declinedFriendRequest = s.declinedFriendRequest ,
                    createdAt = s.createdAt
                })
                .FirstOrDefaultAsync();
            if(response != null )
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<Settings?> GetByUserId(Guid id)
        {
            var response = await _context.dbSettings
                .Where(x => x.attachedUserId == id)
                .Select(s => new Settings
                {
                    id = s.id,
                    bigSaleNotification = s.bigSaleNotification,
                    saleFromWishlistNotification = s.saleFromWishlistNotification,
                    newCommentNotification = s.newCommentNotification,
                    friendRequestNotification = s.friendRequestNotification,
                    approvedFriendRequest = s.approvedFriendRequest,
                    declinedFriendRequest = s.declinedFriendRequest,
                    createdAt = s.createdAt
                })
                .FirstOrDefaultAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Settings?>> GetByIds(List<Guid> ids)
        {
            List<Settings> response = new List<Settings> ();

            foreach(var id in ids)
            {
                var result = await _context.dbSettings
                    .Where(x => x.attachedUserId == id)
                    .Select(s => new Settings
                    {
                        id = s.id,
                        bigSaleNotification = s.bigSaleNotification,
                        saleFromWishlistNotification = s.saleFromWishlistNotification,
                        newCommentNotification = s.newCommentNotification,
                        friendRequestNotification = s.friendRequestNotification,
                        approvedFriendRequest = s.approvedFriendRequest,
                        declinedFriendRequest = s.declinedFriendRequest,
                        createdAt = s.createdAt
                    })
                    .FirstOrDefaultAsync();

                if(result != null)
                {
                    response.Add(result);
                }
            }
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }
    }
}
