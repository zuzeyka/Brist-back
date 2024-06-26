using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.GameInShopRepository;
using Slush.Entity.Store.Product;
using Slush.Models.ShopContent;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameBundleController : Controller
    {
        private readonly GameBundleRepository _gameBundleRepositories;

        public GameBundleController(GameBundleRepository gameBundleRepositories)
        {
            _gameBundleRepositories = gameBundleRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameBundleRepository>>> GetAllGameBundles()
        {
            var _bundles = await _gameBundleRepositories.GetAll();

            return Ok(_bundles);
        }

        [HttpPost]
        public async Task<ActionResult<GameBundle>> CreateGameBundle([FromBody] GameBundleModel model)
        {
            var result = new GameBundle(Guid.NewGuid(),
                model.name,
                model.description,
                model.price,
                model.discount,
                model.discountFinish,
                DateTime.Now);

            var response = _gameBundleRepositories.Add(result);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameBundle>> GetGameBundle(Guid id)
        {
            var response = await _gameBundleRepositories.GetById(id);
            if(response == null)
            {
                return NotFound();
            }    
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameBundle(Guid id)
        {
            await _gameBundleRepositories.DeleteGameBundle(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGameBundle(Guid id, [FromBody] GameBundleModel model)
        {
            var result = await _gameBundleRepositories.UpdateGameBundle(new GameBundle(id, model.name, model.description, model.price, model.discount, model.discountFinish, DateTime.Now));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<GameBundle>>> GetAllGameBundlesByIds([FromBody] List<Guid> guidList)
        {
            var response = await _gameBundleRepositories.GetByBundleIds(guidList);

            return Ok(response);
        }
    }
}
