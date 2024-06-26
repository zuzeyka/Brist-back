using FullStackBrist.Server.Models.Language;
using Microsoft.AspNetCore.Mvc;
using Slush.Repositories.LanguageRepository;
using Slush.Data.Entity;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguageInGameController : Controller
    {
        private readonly LanguageInGameRepository _languageInGameRepositories;

        public LanguageInGameController(LanguageInGameRepository languageInGameRepositories)
        {
            _languageInGameRepositories = languageInGameRepositories;
        }

        [HttpGet]
        public async Task<ActionResult<List<LanguageInGameRepository>>> GetAllLanguagesInGame()
        {
            var _languages = await _languageInGameRepositories.GetAllLanguageInGames();

            return Ok(_languages);
        }


        [HttpPost]
        public async Task<ActionResult<LanguageInGame>> CreateLanguageInGame([FromBody] LanguageInGame model)
        {
            var result = new LanguageInGame(Guid.NewGuid(),
                                            model.gameId,
                                            model.languageId,
                                            DateTime.Now
                                            );
            await _languageInGameRepositories.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LanguageInGame>> GetLanguageInGame(Guid id)
        {
            var response = await _languageInGameRepositories.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLanguageInGame(Guid id)
        {
            await _languageInGameRepositories.DeleteLanguageInGame(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLanguageInGame(Guid id, [FromBody] LanguageInGameModel language)
        {
            var result = await _languageInGameRepositories.UpdateLanguageInGame(new LanguageInGame(id, language.gameId, language.languageId, language.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<LanguageInGame>>> GetAllLanguagesInGameByIds([FromBody] List<Guid> guidList)
        {
            var response = await _languageInGameRepositories.GetByIds(guidList);

            return Ok(response);
        }
    }
}
