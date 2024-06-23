using FullStackBrist.Server.Models.Group;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GroupDao;
using Slush.Data.Entity.Community;
using Slush.Services.Minio;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly PostDao _postDao;
        private readonly MinioService _minioService;

        public PostController(PostDao postDao, MinioService minioService)
        {
            _postDao = postDao;
            _minioService = minioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostDao>>> GetAllPosts()
        {
            var _posts = await _postDao.GetAllPosts();

            return Ok(_posts);
        }


        [HttpPost]
        public async Task<ActionResult<Post>> CreatePost([FromBody] PostModel model, IFormFile? file)
        {
            var result = new Post(Guid.NewGuid(),
                model.title,
                model.description,
                0,
                model.discussionId,
               model.authorId,
               model.content,
               model.contentUrl,
                                            DateTime.Now
                                            );


            if (file != null || file.Length != 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", result.id, file.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);

                        result.contentUrl = url;
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }

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
        public async Task<ActionResult> UpdatePost(Guid id, [FromBody] PostModel post, IFormFile? file)
        {

            if (file != null || file.Length != 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", id, file.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);

                        post.contentUrl = url;
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }

            var result = await _postDao.UpdatePost(new Post(id, post.title, post.description, post.likesCount, post.discussionId, post.authorId, post.content, post.contentUrl, post.createdAt));
            return Ok(result);
        }

        [HttpGet("byattachedid/{id}")]
        public async Task<ActionResult<List<Post>>> GetByAttachedId(Guid id)
        {
            var response = await _postDao.GetByAttachedId(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }


        [HttpPost("getall")]
        public async Task<ActionResult<List<Post>>> GetAllPostsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _postDao.GetByIds(guidList);

            return Ok(response);
        }
    }
}
