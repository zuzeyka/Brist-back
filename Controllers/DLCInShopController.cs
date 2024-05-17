using FullStackBrist.Server.Models.Categories;
using FullStackBrist.Server.Models.Creators;
using FullStackBrist.Server.Models.ShopContent;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Operators;
using Slush.DAO.CreatorsDao;
using Slush.DAO.GameInShopDao;
using Slush.Data;
using Slush.Data.Entity;
using Slush.Entity.Store.Product;
using Slush.Entity.Store.Product.Creators;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DLCInShopController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly DLCInShopDao _dLCInShopDao;

        public DLCInShopController(DataContext dataContext, DLCInShopDao dLCInShopDao)
        {
            _dataContext = dataContext;
            _dLCInShopDao = dLCInShopDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<DLCInShopDao>>> GetAllDlcs()
        {
            var dlcs = await _dLCInShopDao.GetAll();

            var response = dlcs.Select(d => new DLCInShop(id: d.id,
                                                          gameId: d.gameId,
                                                          name: d.name,
                                                          price: d.price,
                                                          discount: d.discount,
                                                          previeImage: d.previeImage,
                                                          dateOfRelease: d.dateOfRelease,
                                                          developerId: d.developerId,
                                                          publisherId: d.publisherId,
                                                          createdAt :d.createdAt)).ToList();
            return Ok(response);
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
            var response = await _dataContext.dbDLCsInShop.AddAsync(result);

            return Ok(response);
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
            var result = new DLCInShop(id, model.gameId, model.name, model.price, model.discount, model.previeImage, model.dateOfRelease, model.developerId, model.publisherId, model.createdAt);
            await _dLCInShopDao.UpdateDLCInShop(result);
            return NoContent();
        }
    }
}
