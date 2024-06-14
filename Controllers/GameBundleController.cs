using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameInShopDao;
using Slush.Entity.Store.Product;
using Slush.Models.ShopContent;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameBundleController : Controller
    {
        private readonly GameBundleDao _gameBundleDao;

        public GameBundleController(GameBundleDao gameBundleDao)
        {
            _gameBundleDao = gameBundleDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<GameBundleDao>>> GetAllGameBundles()
        {
            var _bundles = await _gameBundleDao.GetAll();

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

            var response = _gameBundleDao.Add(result);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameBundle>> GetGameBundle(Guid id)
        {
            var response = await _gameBundleDao.GetById(id);
            if(response == null)
            {
                return NotFound();
            }    
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameBundle(Guid id)
        {
            await _gameBundleDao.DeleteGameBundle(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGameBundle(Guid id, [FromBody] GameBundleModel model)
        {
            await _gameBundleDao.UpdateGameBundle(new GameBundle(id, model.name, model.description, model.price, model.discount, model.discountFinish, DateTime.Now));
            return NoContent();
        }
    }
}
