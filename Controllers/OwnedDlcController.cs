using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.ProfileRepository;
using Slush.Entity.Profile;
using System.Reactive.Subjects;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnedDlcController : Controller
    {
        private readonly OwnedDlcRepository _Repositories;

        public OwnedDlcController(OwnedDlcRepository Repositories)
        {
            _Repositories = Repositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<OwnedGameRepository>>> GetAllOwnedDlcs()
        {
            var dlcs = await _Repositories.GetAllDlcs();

            return Ok(dlcs);
        }

        [HttpPost]
        public async Task<ActionResult<OwnedDlc>> CreateOwnedDlc([FromBody] OwnedDlc entity)
        {
            var result = new OwnedDlc(Guid.NewGuid(),
                entity.ownedDlcId,
                entity.userId,
                DateTime.Now);

            await _Repositories.Add(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOwnedDlc(Guid id)
        {
            await _Repositories.Delete(id);

            return NoContent();
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateOwnedDlc(Guid id, [FromBody] OwnedDlc dlc)
        {
            var result = await _Repositories.UpdateOwned(new OwnedDlc(id, dlc.ownedDlcId, dlc.userId, dlc.createdAt));

            return Ok(result);
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
