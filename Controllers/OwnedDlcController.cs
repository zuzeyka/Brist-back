using Microsoft.AspNetCore.Mvc;
using Slush.DAO.ProfileDao;
using Slush.Entity.Profile;
using System.Reactive.Subjects;

namespace Slush.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnedDlcController : Controller
    {
        private readonly OwnedDlcDao _dao;

        public OwnedDlcController(OwnedDlcDao dao)
        {
            _dao = dao;
        }

        [HttpGet]
        public async Task<ActionResult<List<OwnedGameDao>>> GetAllOwnedDlcs()
        {
            var dlcs = await _dao.GetAllDlcs();

            return Ok(dlcs);
        }

        [HttpPost]
        public async Task<ActionResult<OwnedDlc>> CreateOwnedDlc([FromBody] OwnedDlc entity)
        {
            var result = new OwnedDlc(Guid.NewGuid(),
                entity.ownedDlcId,
                entity.userId,
                DateTime.Now);

            await _dao.Add(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOwnedDlc(Guid id)
        {
            await _dao.Delete(id);

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateOwnedDlc(Guid id, [FromBody] OwnedDlc dlc)
        {
            var result = await _dao.UpdateOwned(new OwnedDlc(id, dlc.ownedDlcId, dlc.userId, dlc.createdAt));

            return Ok(result);
        }

        [HttpGet("byuserid")]
        public async Task<ActionResult<List<OwnedDlc>>> GetOwnedDlcByUserId(Guid id)
        {
            var response = await _dao.GetByUserId(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("byid")]
        public async Task<ActionResult<OwnedDlc>> GetOwnedDlcById(Guid id)
        {
            var response = await _dao.GetById(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<OwnedDlc>>> GetAllOwnedDlcsByIds([FromBody] List<Guid> guidList)
        {
            var response = await _dao.GetByIds(guidList);

            return Ok(response); 
        }
    }
}
