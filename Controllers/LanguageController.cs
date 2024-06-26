using FullStackBrist.Server.Models.Language;
using Microsoft.AspNetCore.Mvc;
using Slush.Data.Entity;
using Slush.Repositories.IRepository;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguageController : Controller
    {
        private readonly ILanguageRepository _LanguageRepository;

        public LanguageController(ILanguageRepository LanguageRepository)
        {
            _LanguageRepository = LanguageRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ILanguageRepository>>> GetAllLanguages()
        {
            var _languages = await _LanguageRepository.GetAllLanguages();

            return Ok(_languages);
        }

        [HttpPost]
        public async Task<ActionResult<Language>> CreateLanguage([FromBody] LanguageModel model)
        {
            var result = new Language(Guid.NewGuid(),
                                            model.name,
                                            DateTime.Now
                                            );
            await _LanguageRepository.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Language>> GetLanguage(Guid id)
        {
            var response = await _LanguageRepository.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLanguage(Guid id)
        {
            await _LanguageRepository.DeleteLanguage(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLanguage(Guid id, [FromBody] LanguageModel language)
        {
            var result = await _LanguageRepository.UpdateLanguage(new Language(id, language.name, language.createdAt));
            return Ok(result);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<List<Language>>> GetAllLanguagesByIds([FromBody] List<Guid> guidList)
        {
            var response = await _LanguageRepository.GetByIds(guidList);

            return Ok(response);
        }
    }
}
