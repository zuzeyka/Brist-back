using FullStackBrist.Server.Models.ShopContent;
using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.GameInShopRepository;
using Slush.Entity.Store.Product;
using Slush.Services.Minio;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DLCInShopController : Controller
    {
        private readonly DLCInShopRepository _DLCInShopRepository;
        private readonly MinioService _minioService;

        public DLCInShopController(DLCInShopRepository DLCInShopRepository, MinioService minioService)
        {
            _DLCInShopRepository = DLCInShopRepository;
            _minioService = minioService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DLCInShopRepository>>> GetAllDlcs()
        {
            var dlcs = await _DLCInShopRepository.GetAll();

            return Ok(dlcs);
        }

        [HttpPost]
        public async Task<ActionResult<DLCInShop>> CreateDLCInShop([FromBody] DLCInShopModel model, IFormFile file)
        {

            var result = new DLCInShop(Guid.NewGuid(),
                                        model.gameId,
                                        model.name,
                                        model.price,
                                        model.discount,
                                        model.discountFinish,
                                        model.previeImage,
                                        model.description,
                                        model.dateOfRelease,
                                        model.developerId,
                                        model.publisherId,
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

                        result.previeImage = url;

                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload image: {ex.Message}");
                    }
                }
            }
            await _DLCInShopRepository.Add(result);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<DLCInShop>> GetDLCInShop(Guid id)
        {
            var response = await _DLCInShopRepository.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("bygameid/{id}")]
        public async Task<ActionResult<List<DLCInShop>>> GetDLCInShopByGameId(Guid id)
        {
            var response = await _DLCInShopRepository.GetByGameId(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDLCInShop(Guid id)
        {
            await _DLCInShopRepository.DeleteDLCInShop(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateDLCInShop(Guid id, [FromBody] DLCInShopModel model, IFormFile file)
        {
            if (file != null || file.Length != 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", id, file.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);

                        var result = await _DLCInShopRepository.UpdateDLCInShop(new DLCInShop(id, model.gameId, model.name, model.price, model.discount, model.discountFinish, url, model.description, model.dateOfRelease, model.developerId, model.publisherId, model.createdAt));
                        return Ok(result);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload image: {ex.Message}");
                    }
                }
            }

            var res = await _DLCInShopRepository.UpdateDLCInShop(new DLCInShop(id, model.gameId, model.name, model.price, model.discount, model.discountFinish, model.previeImage, model.description, model.dateOfRelease, model.developerId, model.publisherId, model.createdAt));
            return Ok(res);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<DLCInShop>>> GetAllDlcByIds([FromBody] List<Guid> guidList)
        {
            var response = await _DLCInShopRepository.GetDlcsByIds(guidList);

            return Ok(response);
        }
    }
}
