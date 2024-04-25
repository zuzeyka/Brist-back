using Slush.Data.Entity.Community;
using Slush.Data;
using Slush.Entity.Profile;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity.Community.GameGroup;

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
        public void Add(Friends friend)
        {
            _context.dbFriends.Add(friend);
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

        public async Task<Friends> GetById(Guid id)
        {
            return await Task.FromResult(_context.dbFriends.FirstOrDefault(f => f.id == id));
        }
    }
}
