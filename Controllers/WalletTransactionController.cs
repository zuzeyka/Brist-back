using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Data;
using Slush.Entity.Profile;
using Slush.Models.Profile;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletTransactionController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly WalletTransactionsDao _walletTransactionsDao;

        public WalletTransactionController(DataContext dataContext, WalletTransactionsDao walletTransactionsDao)
        {
            _dataContext = dataContext;
            _walletTransactionsDao = walletTransactionsDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<WalletTransactionsDao>>> GetAll()
        {
            var walletTransactions = await _walletTransactionsDao.GetAll();

            var response = walletTransactions.Select(w => new WalletTransactions(
                id: w.id,
                userId:  w.userId,
                transactionObj: w.transactionObj,
                currency:  w.currency,
                createdAt: w.createdAt)).ToList();

            return Ok(response);
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

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] WalletTransactions model)
        {
            var transaction = new WalletTransactions(id,
                model.userId,
                model.transactionObj,
                model.currency,
                DateTime.Now);

            await _walletTransactionsDao.UpdateWalletTransactions(transaction);

            return NoContent();
        }
    }
}
