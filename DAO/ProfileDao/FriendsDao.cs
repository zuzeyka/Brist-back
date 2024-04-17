using Slush.Data.Entity.Community;
using Slush.Data;
using Slush.Entity.Profile;
using Microsoft.EntityFrameworkCore;

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
            return await _context.dbFriends.Select(f => new Friends {
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
    }
}
