using FullStackBrist.Server.Models.ShopContent;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameInShopDao;
using Slush.Entity.Store.Product;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesInShopController : Controller
    {
        private readonly GameInShopDao _gameInShopDao;

        public GamesInShopController(GameInShopDao gameInShopDao)
        {
            _gameInShopDao = gameInShopDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameInShopDao>>> GetAllGames()
        {
            var games = await _gameInShopDao.GetAll();
        
            return Ok(_gameInShopDao);
        }

        [HttpPost]
        public async Task<ActionResult<GameInShop>> CreateGameInShop([FromBody] GameInShopModel model)
        {
            var result = new GameInShop(Guid.NewGuid(),
                                            model.name,
                                            model.price,
                                            model.discount,
                                            model.previeImage,
                                            model.dateOfRelease,
                                            model.developerId,
                                            model.publisherId,
                                            model.urlForContent,
                                            DateTime.Now
                                            );
            await _gameInShopDao.Add(result);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<GameInShop>> GetGameInShop(Guid id)
        {
            var response = await _gameInShopDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameInShop(Guid id)
        {
            await _gameInShopDao.DeleteGameInShop(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGameNews(Guid id, [FromBody] GameInShopModel game)
        {
            await _gameInShopDao.UpdateGameInShop(new GameInShop(id, game.name, game.price, game.discount, game.previeImage, game.dateOfRelease, game.developerId, game.publisherId, game.urlForContent, game.createdAt));
            return NoContent();
        }
    }
}
