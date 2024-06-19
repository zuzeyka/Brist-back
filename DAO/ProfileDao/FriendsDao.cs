using Slush.Data.Entity.Community;
using Slush.Data;
using Slush.Entity.Profile;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity.Community.GameGroup;
using Slush.Data.Entity;

namespace Slush.DAO.ProfileDao
{
    public class FriendsDao
    {
        private readonly DataContext _context;

        public FriendsDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Friends>> GetAllFriends()
        {
            return await _context.dbFriends
                .Where(f => f.deleteAt == null)
                .Select(f => new Friends {
                id = f.id,
                userId = f.userId,
                friendId = f.friendId,
                createdAt = f.createdAt}).ToListAsync();

        }

        public async Task<Friends> UpdateFriends(Friends friends)
        {
            var existing = await _context.dbFriends.FindAsync(friends.id);
            if (existing != null)
            {
                existing.friendId = friends.id;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(Friends friend)
        {
            await _context.dbFriends.AddAsync(friend);
            _context.SaveChanges();
        }

        public async Task DeleteFriends(Guid id)
        {
            var requirement = await _context.dbFriends.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Friends?> GetById(Guid id)
        {
            var response = await _context.dbFriends
                .Where(x => x.id == id)
                .Select(f => new Friends
                {
                    id = f.id,
                    userId = f.userId,
                    friendId = f.friendId,
                    createdAt = f.createdAt
                }).FirstOrDefaultAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<Friends?>> GetByUserId(Guid id)
        {
            var response = await _context.dbFriends
                .Where(x => x.userId == id)
                .Where(a => a.deleteAt == null)
                .Select(f => new Friends
                {
                    id = f.id,
                    userId = f.userId,
                    friendId = f.friendId,
                    createdAt = f.createdAt
                }).ToListAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<Friends?>> GetByUserIds(List<Guid> id)
        {
            List<Friends> response = new List<Friends> ();

            foreach(var i in id)
            {
                var result = await _context.dbFriends
                .Where(x => x.id == i)
                .Where(a => a.deleteAt == null)
                .Select(f => new Friends
                {
                    id = f.id,
                    userId = f.userId,
                    friendId = f.friendId,
                    createdAt = f.createdAt
                }).FirstOrDefaultAsync();

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
