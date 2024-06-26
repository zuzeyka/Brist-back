using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.ProfileRepository;
using Slush.Entity.Profile;
using Slush.Models.Profile;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletTransactionController : Controller
    {
        private readonly WalletTransactionsRepository _walletTransactionsRepositories;

        public WalletTransactionController(WalletTransactionsRepository walletTransactionsRepositories)
        {
            _walletTransactionsRepositories = walletTransactionsRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<WalletTransactionsRepository>>> GetAll()
        {
            var walletTransactions = await _walletTransactionsRepositories.GetAll();

            return Ok(walletTransactions);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<WalletTransactions>> GetById(Guid id)
        {
            var response = await _walletTransactionsRepositories.GetById(id);

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
            var response = await _walletTransactionsRepositories.GetById(userId);

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

            await _walletTransactionsRepositories.Add(transaction);

            return Ok(transaction);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWalletTransactions(Guid id)
        {
            await _walletTransactionsRepositories.DeleteWalletTransaction(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] WalletTransactions model)
        {
            var result = await _walletTransactionsRepositories.UpdateWalletTransactions(new WalletTransactions(id,
                model.userId,
                model.transactionObj,
                model.currency,
                DateTime.Now));

            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<WalletTransactions>>> GetAllWalletTransactionsByIds([FromBody] List<Guid> ids)
        {
            var response = await _walletTransactionsRepositories.GetByIds(ids);

            return Ok(response);
        }
    }
}
