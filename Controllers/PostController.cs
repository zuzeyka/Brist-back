using FullStackBrist.Server.Models.Group;
using FullStackBrist.Server.Models.Profile;
using FullStackBrist.Server.Models.Requirements;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GroupDao;
using Slush.DAO.RequirementsDao;
using Slush.Data;
using Slush.Data.Entity;
using Slush.Data.Entity.Community;
using Slush.Entity.Profile;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

            var response = _posts.Select(p => new Post(id: p.id,
                                                       title: p.title,
                                                       description: p.description,
                                                       likesCount: p.likesCount,
                                                       discussionId: p.discussionId,
                                                       authorId: p.authorId,
                                                       content: p.content,
                                                       createdAt: p.createdAt)).ToList();

            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost([FromBody] PostModel model)
        {
            var result = new Post(Guid.NewGuid(),
                model.title,
                model.description,
                0,
                model.discussionId,
               model.authorId,
               model.content,
                                            DateTime.Now
                                            );
            var response = await _dataContext.dbPosts.AddAsync(result);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(Guid id)
        {
            var response = await _postDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePost(Guid id)
        {
            await _postDao.DeletePost(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePost(Guid id, [FromBody] PostModel post)
        {
            var result = new Post(id, post.title, post.description, post.likesCount,  post.discussionId,  post.authorId, post.content, post.createdAt);
            await _postDao.UpdatePost(result);
            return NoContent();
        }
    }
}
