using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Operators;
using Slush.DAO.GameInShopDao;
using Slush.Data;
using Slush.Entity.Store.Product;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[dlcController]")]
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
                                                                     d.publisherId)).ToList();
            return Ok(response);
        }
    }
}
