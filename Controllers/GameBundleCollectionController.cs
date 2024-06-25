using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.GameInShopRepository;
using Slush.Entity.Store.Product;
using Slush.Models.ShopContent;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameBundleCollectionController : Controller
    {
        private readonly GameBundleCollectionRepository _Repositories;

        public GameBundleCollectionController(GameBundleCollectionRepository Repositories)
        {
            _Repositories = Repositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameBundleCollectionRepository>>> GetAllCollections()
        {
            var _bundles = await _Repositories.GetAll();

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

            var response = _Repositories.Add(result);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameBundleCollection>> GetCollection(Guid id)
        {
            var response = await _Repositories.GetById(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("bygameid/{id}")]
        public async Task<ActionResult<List<GameBundleCollection>>> GetCollectionByGameId(Guid id)
        {
            var response = await _Repositories.GetByGameId(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _Repositories.DeleteGameBundleCollection(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] GameBundleCollectionModel model)
        {
            var result = await _Repositories.UpdateGameBundleCollection(new GameBundleCollection(id, model.gameId, model.dlcId, model.bundleId, DateTime.Now));

            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<GameBundleCollection>>> GetAllBundleCollectionsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _Repositories.GetByGameIds(guidList);

            return Ok(response);
        }
    }
}
