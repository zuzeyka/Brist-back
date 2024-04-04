using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data;
using Slush.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[friendsController]")]
    public class FriendsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly FriendsDao _friendsDao;

        public FriendsController(DataContext dataContext, FriendsDao friendsDao)
        {
            _dataContext = dataContext;
            _friendsDao = friendsDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<FriendsDao>>> GetAllFriends()
        {
            var _friends = await _friendsDao.GetAllFriends();

            var response = _friends.Select(f => new Friends(f.id,
                                                                   f.userId,
                                                                   f.friendId)).ToList();

            return Ok(response);
        }
    }
}
