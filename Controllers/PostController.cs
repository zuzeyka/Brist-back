using FullStackBrist.Server.Models.Group;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GroupDao;
using Slush.Data.Entity.Community;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly PostDao _postDao;

        public PostController(PostDao postDao)
        {
            _postDao = postDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostDao>>> GetAllPosts()
        {
            var _posts = await _postDao.GetAllPosts();

            return Ok(_posts);
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
            await _postDao.Add(result);
            return Ok(result);
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
            await _postDao.UpdatePost(new Post(id, post.title, post.description, post.likesCount, post.discussionId, post.authorId, post.content, post.createdAt));
            return NoContent();
        }
    }
}
