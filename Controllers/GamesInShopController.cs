using FullStackBrist.Server.Models.ShopContent;
using Microsoft.AspNetCore.Mvc;
using Slush.Entity.Store.Product;
using Slush.Services.Minio;
using Slush.Repositories.IRepository;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesInShopController : Controller
    {
        private readonly IGameInShopRepository _GameInShopRepository;
        private readonly IMinioService _minioService;

        public GamesInShopController(IGameInShopRepository GameInShopRepository, IMinioService minioService)
        {
            _GameInShopRepository = GameInShopRepository;
            _minioService = minioService;
        }
        [HttpGet]
        public async Task<ActionResult<List<IGameInShopRepository>>> GetAllGames()
        {
            var games = await _GameInShopRepository.GetAll();
        
            return Ok(games);
        }
        [HttpPost]
        public async Task<ActionResult<GameInShop>> CreateGameInShop([FromBody] GameInShopModel model, IFormFile file)
        {
            var result = new GameInShop(Guid.NewGuid(),
                                            model.name,
                                            model.price,
                                            model.discount,
                                            model.discountFinish,
                                            model.previeImage,
                                            model.description,
                                            model.dateOfRelease,
                                            model.developerId,
                                            model.publisherId,
                                            model.urlForContent,
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
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }
            await _GameInShopRepository.Add(result);

            return Ok(result);
        }
        [HttpGet("byname/{name}")]
        public async Task<ActionResult<GameInShop>> GetByName(String name)
        {
            var response = await _GameInShopRepository.GetGameInShopByName(name);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameInShop>> GetGameInShop(Guid id)
        {
            var response = await _GameInShopRepository.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameInShop(Guid id)
        {
            await _GameInShopRepository.DeleteGameInShop(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGameNews(Guid id, [FromBody] GameInShopModel game, IFormFile file)
        {
            if(file != null || file.Length != 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    try
                    {
                        String imageUrl = await _minioService.SaveFile("images", id, file.FileName, stream);

                        var url = await _minioService.GetUrlToFile(imageUrl);
                        game.previeImage = url;

                        var res = await _GameInShopRepository.UpdateGameInShop(new GameInShop(id, game.name, game.price, game.discount, game.discountFinish, game.previeImage, game.description, game.dateOfRelease, game.developerId, game.publisherId, game.urlForContent, game.createdAt));

                        return Ok(res);
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Failed to upload file: {ex.Message}");
                    }
                }
            }

            var result = await _GameInShopRepository.UpdateGameInShop(new GameInShop(id, game.name, game.price, game.discount, game.discountFinish, game.previeImage, game.description, game.dateOfRelease, game.developerId, game.publisherId, game.urlForContent, game.createdAt));
            return Ok(result);
        }
        [HttpPost("getall")]
        public async Task<ActionResult<List<GameInShop>>> GetAllGamesInShopByIds([FromBody] List<Guid> ids)
        {
            var response = await _GameInShopRepository.GetByIds(ids);

            return Ok(response);
        }
    }
}
