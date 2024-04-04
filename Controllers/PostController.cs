using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GroupDao;
using Slush.Data;
using Slush.Data.Entity.Community;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[postController]")]
    public class PostController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly PostDao _postDao;

        public PostController(DataContext dataContext, PostDao postDao)
        {
            _dataContext = dataContext;
            _postDao = postDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostDao>>> GetAllPosts()
        {
            var _posts = await _postDao.GetAllPosts();

            var response = _posts.Select(p => new Post(p.id,
                                                           p.title,
                                                           p.description,
                                                           p.likesCount,
                                                           p.dislikesCount,
                                                           p.discussionId,
                                                           p.gameId,
                                                           p.authorId,
                                                           p.content)).ToList();

            return Ok(response);
        }
    }
}
