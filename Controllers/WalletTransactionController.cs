using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slush.Entity.Profile;
using Slush.Models.Profile;
using Slush.Repositories.IRepository;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WalletTransactionController : Controller
    {
        private readonly IWalletTransactions _walletTransactionsRepositories;

        public WalletTransactionController(IWalletTransactions walletTransactionsRepositories)
        {
            _walletTransactionsRepositories = walletTransactionsRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<WalletTransactions>>> GetAll()
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            var walletTransactions = await _walletTransactionsRepositories.GetByUserId(currentUserId.Value);

            return Ok(walletTransactions);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<WalletTransactions>> GetById(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var response = await _walletTransactionsRepositories.GetById(id);

            if(response == null)
            {
                return NotFound();
            }
            if (currentUserId == null || response.userId != currentUserId.Value)
            {
                return Forbid();
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("getbyuid/{id}")]
        public async Task<ActionResult<List<WalletTransactions>>> GetByUserId(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null || id != currentUserId.Value)
            {
                return Forbid();
            }

            var response = await _walletTransactionsRepositories.GetByUserId(id);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<WalletTransactions>> CreateWalletTransaction([FromBody] WalletTransactionsModel model)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            // The caller can only ever create a transaction for themselves —
            // client-supplied model.userId is ignored to prevent crediting/debiting others.
            var transaction = new WalletTransactions(Guid.NewGuid(),
                currentUserId.Value,
                model.transactionObj,
                model.currency,
                DateTime.Now);

            await _walletTransactionsRepositories.Add(transaction);

            return Ok(transaction);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWalletTransactions(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _walletTransactionsRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.userId != currentUserId.Value)
            {
                return Forbid();
            }

            await _walletTransactionsRepositories.DeleteWalletTransaction(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] WalletTransactions model)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _walletTransactionsRepositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.userId != currentUserId.Value)
            {
                return Forbid();
            }

            // userId is intentionally not taken from the client — ownership of a
            // transaction can never be reassigned through this endpoint.
            var result = await _walletTransactionsRepositories.UpdateWalletTransactions(new WalletTransactions(id,
                existing.userId,
                model.transactionObj,
                model.currency,
                DateTime.Now));

            return Ok(result);
        }

        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst("userId")?.Value;
            return Guid.TryParse(claim, out var currentUserId) ? currentUserId : null;
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<WalletTransactions>>> GetAllWalletTransactionsByIds([FromBody] List<Guid> ids)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            var response = await _walletTransactionsRepositories.GetByIds(ids);

            // Only the caller's own transactions are ever returned, regardless of
            // which ids were requested.
            return Ok(response.Where(t => t != null && t.userId == currentUserId.Value).ToList());
        }
    }
}
