using FullStackBrist.Server.Models.Categories;
using FullStackBrist.Server.Models.ShopContent;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Operators;
using Slush.DAO.GameInShopDao;
using Slush.Data;
using Slush.Data.Entity;
using Slush.Entity.Store.Product;

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

            var response = dlcs.Select(d => new DLCInShop(d.id,
                                                                     d.gameId,
                                                                     d.name,
                                                                     d.price,
                                                                     d.discount,
                                                                     d.previeImage,
                                                                     d.gameImages,
                                                                     d.dateOfRelease,
                                                                     d.developerId,
                                                                     d.publisherId,
                                                                     d.createdAt)).ToList();
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
                                        model.gameImages,
                                        model.dateOfRelease,
                                        model.developerId,
                                        model.publisherId,
                                        DateTime.Now
                                            );
            _dataContext.dbDLCsInShop.AddAsync(result);

            return result;
        }
    }
}
