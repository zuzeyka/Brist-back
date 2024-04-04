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
            var _friendsEntity = await _context.dbFriends.AsNoTracking().ToListAsync();

            var _friends = _friendsEntity.Select(f => new Friends(f.id,
                                                                   f.userId,
                                                                   f.friendId)).ToList();
            return _friends;
        }

        public void Add(Friends friend)
        {
            _context.dbFriends.Add(friend);
            _context.SaveChanges();
        }
    }
}
