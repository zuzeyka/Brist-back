using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Entity.Profile;
using Slush.Models.Profile;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletTransactionController : Controller
    {
        private readonly WalletTransactionsDao _walletTransactionsDao;

        public WalletTransactionController(WalletTransactionsDao walletTransactionsDao)
        {
            _walletTransactionsDao = walletTransactionsDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<WalletTransactionsDao>>> GetAll()
        {
            var walletTransactions = await _walletTransactionsDao.GetAll();

            return Ok(walletTransactions);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<WalletTransactions>> GetById(Guid id)
        {
            var response = await _walletTransactionsDao.GetById(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("getbyuid/{id}")]
        public async Task<ActionResult<WalletTransactions>> GetByUserId(Guid userId)
        {
            var response = await _walletTransactionsDao.GetById(userId);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<WalletTransactions>> CreateWalletTransaction([FromBody] WalletTransactionsModel model)
        {
            var transaction = new WalletTransactions(Guid.NewGuid(),
                model.userId,
                model.transactionObj,
                model.currency,
                DateTime.Now);

            await _walletTransactionsDao.Add(transaction);

            return Ok(transaction);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWalletTransactions(Guid id)
        {
            await _walletTransactionsDao.DeleteWalletTransaction(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] WalletTransactions model)
        {
            var result = await _walletTransactionsDao.UpdateWalletTransactions(new WalletTransactions(id,
                model.userId,
                model.transactionObj,
                model.currency,
                DateTime.Now));

            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<WalletTransactions>>> GetAllWalletTransactionsByIds([FromBody] List<Guid> ids)
        {
            var response = await _walletTransactionsDao.GetByIds(ids);

            return Ok(response);
        }
    }
}
