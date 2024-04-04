using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameInShopDao;
using Slush.Data;
using Slush.Entity.Store.Product;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[gamesController]")]
    public class GamesInShopController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly GameInShopDao _gameInShopDao;

        public GamesInShopController(DataContext dataContext, GameInShopDao gameInShopDao)
        {
            _dataContext = dataContext;
            _gameInShopDao = gameInShopDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameInShopDao>>> GetAllGames()
        {
            var games = await _gameInShopDao.GetAll();

            var response = games.Select(g => new GameInShop(g.id,
                                                                        g.name,
                                                                        g.price,
                                                                        g.discount,
                                                                        g.previeImage,
                                                                        g.dateOfRelease,
                                                                        g.developerId,
                                                                        g.publisherId,
                                                                        g.urlForContent)).ToList();
            return Ok(response);
        }
            
    }
}
