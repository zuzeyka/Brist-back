using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameInShopDao;
using Slush.Entity.Store.Product;
using Slush.Models.ShopContent;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameBundleCollectionController : Controller
    {
        private readonly GameBundleCollectionDao _dao;

        public GameBundleCollectionController(GameBundleCollectionDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameBundleCollectionDao>>> GetAllCollections()
        {
            var _bundles = await _dao.GetAll();

            return Ok(_bundles);
        }

        [HttpPost]
        public async Task<ActionResult<GameBundleCollection>> CreateCollection([FromBody] GameBundleCollectionModel model)
        {
            var result = new GameBundleCollection(Guid.NewGuid(),
                model.gameId,
                model.dlcId,
                model.bundleId,
                DateTime.Now);

            var response = _dao.Add(result);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameBundleCollection>> GetCollection(Guid id)
        {
            var response = await _dao.GetById(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("bygameid/{id}")]
        public async Task<ActionResult<List<GameBundleCollection>>> GetCollectionByGameId(Guid id)
        {
            var response = await _dao.GetByGameId(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _dao.DeleteGameBundleCollection(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] GameBundleCollectionModel model)
        {
            var result = await _dao.UpdateGameBundleCollection(new GameBundleCollection(id, model.gameId, model.dlcId, model.bundleId, DateTime.Now));

            return Ok(result);
        }
    }
}
