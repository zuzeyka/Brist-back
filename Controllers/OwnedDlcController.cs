using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slush.Entity.Profile;
using Slush.Repositories.IRepository;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OwnedDlcController : Controller
    {
        private readonly IOwnedDlcRepository _Repositories;

        public OwnedDlcController(IOwnedDlcRepository Repositories)
        {
            _Repositories = Repositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<IOwnedDlcRepository>>> GetAllOwnedDlcs()
        {
            var dlcs = await _Repositories.GetAllDlcs();

            return Ok(dlcs);
        }

        [HttpPost]
        public async Task<ActionResult<OwnedDlc>> CreateOwnedDlc([FromBody] OwnedDlc entity)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == null)
            {
                return Forbid();
            }

            // Ownership can only ever be granted to the authenticated caller here;
            // client-supplied entity.userId is ignored to prevent granting DLC to others.
            var result = new OwnedDlc(Guid.NewGuid(),
                entity.ownedDlcId,
                currentUserId.Value,
                DateTime.Now);

            await _Repositories.Add(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOwnedDlc(Guid id)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _Repositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.userId != currentUserId.Value)
            {
                return Forbid();
            }

            await _Repositories.Delete(id);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateOwnedDlc(Guid id, [FromBody] OwnedDlc dlc)
        {
            var currentUserId = GetCurrentUserId();
            var existing = await _Repositories.GetById(id);
            if (existing == null)
            {
                return NotFound();
            }
            if (currentUserId == null || existing.userId != currentUserId.Value)
            {
                return Forbid();
            }

            // userId is intentionally not taken from the client — ownership of a
            // grant can never be reassigned to another user through this endpoint.
            var result = await _Repositories.UpdateOwned(new OwnedDlc(id, dlc.ownedDlcId, existing.userId, dlc.createdAt));

            return Ok(result);
        }

        private Guid? GetCurrentUserId()
        {
            var claim = User.FindFirst("userId")?.Value;
            return Guid.TryParse(claim, out var currentUserId) ? currentUserId : null;
        }

        [HttpGet("byuserid/{id}")]
        public async Task<ActionResult<List<OwnedDlc>>> GetOwnedDlcByUserId(Guid id)
        {
            var response = await _Repositories.GetByUserId(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("byid/{id}")]
        public async Task<ActionResult<OwnedDlc>> GetOwnedDlcById(Guid id)
        {
            var response = await _Repositories.GetById(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<OwnedDlc>>> GetAllOwnedDlcsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _Repositories.GetByIds(guidList);

            return Ok(response); 
        }
    }
}
