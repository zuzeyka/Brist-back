using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slush.Entity.Profile;
using Slush.Repositories.IRepository;

namespace Slush.Controllers
{
    public class CheckoutItemModel
    {
        public Guid itemId { get; set; }
        // "game" or "dlc" — anything else is rejected.
        public string itemType { get; set; } = "game";
    }

    public class CheckoutResultModel
    {
        public float newBalance { get; set; }
        public List<Guid> purchasedItemIds { get; set; } = new();
    }

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PurchaseController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IGameInShopRepository _gameInShopRepository;
        private readonly IDLCInShopRepository _dlcInShopRepository;
        private readonly IOwnedGameRepository _ownedGameRepository;
        private readonly IOwnedDlcRepository _ownedDlcRepository;
        private readonly IWalletTransactions _walletTransactionsRepository;

        public PurchaseController(
            IUserRepository userRepository,
            IGameInShopRepository gameInShopRepository,
            IDLCInShopRepository dlcInShopRepository,
            IOwnedGameRepository ownedGameRepository,
            IOwnedDlcRepository ownedDlcRepository,
            IWalletTransactions walletTransactionsRepository)
        {
            _userRepository = userRepository;
            _gameInShopRepository = gameInShopRepository;
            _dlcInShopRepository = dlcInShopRepository;
            _ownedGameRepository = ownedGameRepository;
            _ownedDlcRepository = ownedDlcRepository;
            _walletTransactionsRepository = walletTransactionsRepository;
        }

        // Covers individual games and DLCs only. Bundles aren't supported here yet —
        // a bundle's price/contents live in GameBundle/GameBundleCollection and the
        // frontend doesn't carry bundle ids through to the cart at all today, so
        // bundle checkout is a separate follow-up, not a corner case of this endpoint.
        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutResultModel>> Checkout([FromBody] List<CheckoutItemModel> items)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            if (items == null || items.Count == 0)
            {
                return BadRequest("Cart is empty.");
            }

            var user = await _userRepository.GetById(currentUserId.Value);
            if (user == null)
            {
                return NotFound();
            }

            var ownedGameIds = (await _ownedGameRepository.GetByUId(currentUserId.Value))
                .Where(g => g != null)
                .Select(g => g!.ownedGameId)
                .ToHashSet();
            var ownedDlcIds = (await _ownedDlcRepository.GetByUserId(currentUserId.Value))
                .Where(d => d != null)
                .Select(d => d!.ownedDlcId)
                .ToHashSet();

            // Every line item's real price and name are resolved from the catalog here —
            // the client never supplies a price, so there is no way to pay less than the
            // current catalog price (discount included) for anything.
            var resolved = new List<(Guid id, bool isDlc, float price)>();
            var alreadyOwned = new List<string>();

            foreach (var item in items)
            {
                if (item.itemType == "dlc")
                {
                    var dlc = await _dlcInShopRepository.GetById(item.itemId);
                    if (dlc == null)
                    {
                        return NotFound($"DLC {item.itemId} not found.");
                    }
                    if (ownedDlcIds.Contains(dlc.id))
                    {
                        alreadyOwned.Add(dlc.name ?? dlc.id.ToString());
                        continue;
                    }
                    resolved.Add((dlc.id, true, FinalPrice(dlc.price, dlc.discount)));
                }
                else if (item.itemType == "game")
                {
                    var game = await _gameInShopRepository.GetById(item.itemId);
                    if (game == null)
                    {
                        return NotFound($"Game {item.itemId} not found.");
                    }
                    if (ownedGameIds.Contains(game.id))
                    {
                        alreadyOwned.Add(game.name ?? game.id.ToString());
                        continue;
                    }
                    resolved.Add((game.id, false, FinalPrice(game.price, game.discount)));
                }
                else
                {
                    return BadRequest($"Unknown itemType '{item.itemType}'.");
                }
            }

            if (alreadyOwned.Count > 0)
            {
                return Conflict(new { message = "Some items are already owned.", items = alreadyOwned });
            }

            var total = resolved.Sum(r => r.price);
            if (user.amountOfMoney < total)
            {
                return StatusCode(StatusCodes.Status402PaymentRequired, new { message = "Insufficient balance.", required = total, balance = user.amountOfMoney });
            }

            // Balance is deducted before any item is granted: if something fails partway
            // through the loop below, the caller is out money for ungranted items rather
            // than walking away with free games.
            user.amountOfMoney -= total;
            await _userRepository.UpdateUser(user);

            foreach (var item in resolved)
            {
                if (item.isDlc)
                {
                    await _ownedDlcRepository.Add(new OwnedDlc(Guid.NewGuid(), item.id, currentUserId.Value, DateTime.Now));
                }
                else
                {
                    await _ownedGameRepository.Add(new OwnedGame(Guid.NewGuid(), item.id, currentUserId.Value, DateTime.Now));
                }

                await _walletTransactionsRepository.Add(new WalletTransactions(Guid.NewGuid(), currentUserId.Value, item.id, -item.price, DateTime.Now));
            }

            return Ok(new CheckoutResultModel
            {
                newBalance = user.amountOfMoney,
                purchasedItemIds = resolved.Select(r => r.id).ToList(),
            });
        }

        private static float FinalPrice(float price, int discount)
        {
            return MathF.Floor(price - price * discount / 100f);
        }

        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst("userId")?.Value;
            return Guid.TryParse(claim, out var currentUserId) ? currentUserId : null;
        }
    }
}
