using FullStackBrist.Server.Models.Group;
using Microsoft.AspNetCore.Mvc;
using Slush.Data.Entity.Community;
using Slush.Services.Minio;
using Slush.Repositories.IRepository;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        private readonly IGroupRepository _GroupRepository;
        private readonly IMinioService _minioService;

        public GroupController(IGroupRepository GroupRepository, IMinioService minioService)
        {
            _GroupRepository = GroupRepository;
            _minioService = minioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<IGroupRepository>>> GetAllGroups()
        {
            var _groups = await _GroupRepository.GetAllGroups();

            return Ok(_groups);
        }


        [HttpPost]
        public async Task<ActionResult<Group>> CreateGroup([FromBody] GroupModel model, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty");
            }
            var result = new Group(Guid.NewGuid(),
                model.attachedId,
                model.name,
                model.description,
                model.imageUrl,
                                            DateTime.Now
                                            );

            using (var stream = file.OpenReadStream())
            {
                try
                {
                    String imageUrl = await _minioService.SaveFile("images", result.id, file.FileName, stream);

                    var url = await _minioService.GetUrlToFile(imageUrl);

                    result.imageUrl = url;

                    await _GroupRepository.Add(result);

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Failed to upload file: {ex.Message}");
                }
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(Guid id)
        {
            var response = await _GroupRepository.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGroup(Guid id)
        {
            await _GroupRepository.DeleteGroup(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGroup(Guid id, [FromBody] GroupModel group, IFormFile file)
        {
            if (file != null || file.Length != 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", id, file.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);

                        group.imageUrl = url;

                        var res = await _GroupRepository.UpdateGroup(new Group(id, group.attachedId, group.name, group.description, group.imageUrl, group.createdAt));
                        return Ok(res);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }
            var result = await _GroupRepository.UpdateGroup(new Group(id, group.attachedId, group.name, group.description, group.imageUrl, group.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Group>>> GetAllGroupsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _GroupRepository.GetByIds(guidList);

            return Ok(response);
        }
    }
}
