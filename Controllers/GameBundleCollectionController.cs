using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameInShopDao;
using Slush.Data;
using Slush.Entity.Store.Product;
using Slush.Models.ShopContent;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameBundleCollectionController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly GameBundleCollectionDao _dao;

        public GameBundleCollectionController(DataContext dataContext, GameBundleCollectionDao dao)
        {
            _dataContext = dataContext;
            _dao = dao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameBundleCollectionDao>>> GetAllCollections()
        {
            var _bundles = await _dao.GetAll();

            var response = _bundles.Select(b => new GameBundleCollection(
                id: b.id,
                gameId: b.gameId,
                bundleId: b.bundleId,
                createdAt: b.createdAt)).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<GameBundleCollection>> CreateCollection([FromBody] GameBundleCollectionModel model)
        {
            var result = new GameBundleCollection(Guid.NewGuid(),
                model.gameId,
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _dao.DeleteGameBundleCollection(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] GameBundleCollectionModel model)
        {
            var result = new GameBundleCollection(id, model.gameId, model.bundleId, DateTime.Now);

            await _dao.UpdateGameBundleCollection(result);

            return NoContent();
        }
    }
}
