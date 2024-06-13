using FullStackBrist.Server.Models.ShopContent;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GameInShopDao;
using Slush.Entity.Store.Product;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DLCInShopController : Controller
    {
        private readonly DLCInShopDao _dLCInShopDao;

        public DLCInShopController(DLCInShopDao dLCInShopDao)
        {
            _dLCInShopDao = dLCInShopDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<DLCInShopDao>>> GetAllDlcs()
        {
            var dlcs = await _dLCInShopDao.GetAll();

            return Ok(dlcs);
        }

        [HttpPost]
        public async Task<ActionResult<DLCInShop>> CreateDLCInShop([FromBody] DLCInShopModel model)
        {
            var result = new DLCInShop(Guid.NewGuid(),
                                        model.gameId,
                                        model.name,
                                        model.price,
                                        model.discount,
                                        model.previeImage,
                                        model.dateOfRelease,
                                        model.developerId,
                                        model.publisherId,
                                        DateTime.Now
                                            );
            await _dLCInShopDao.Add( result );

            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<DLCInShop>> GetDLCInShop(Guid id)
        {
            var response = await _dLCInShopDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDLCInShop(Guid id)
        {
            await _dLCInShopDao.DeleteDLCInShop(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDLCInShop(Guid id, [FromBody] DLCInShopModel model)
        {
            await _dLCInShopDao.UpdateDLCInShop(new DLCInShop(id, model.gameId, model.name, model.price, model.discount, model.previeImage, model.dateOfRelease, model.developerId, model.publisherId, model.createdAt));
            return NoContent();
        }
    }
}
