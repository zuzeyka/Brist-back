using FullStackBrist.Server.Models.Language;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.LanguageDao;
using Slush.Data;
using Slush.Data.Entity;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguageController : Controller
    {
        private readonly LanguageDao _languageDao;

        public LanguageController(LanguageDao languageDao)
        {
            _languageDao = languageDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<LanguageDao>>> GetAllLanguages()
        {
            var _languages = await _languageDao.GetAllLanguages();

            return Ok(_languages);
        }

        [HttpPost]
        public async Task<ActionResult<Language>> CreateLanguage([FromBody] LanguageModel model)
        {
            var result = new Language(Guid.NewGuid(),
                                            model.name,
                                            DateTime.Now
                                            );
            await _languageDao.Add(result);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Language>> GetLanguage(Guid id)
        {
            var response = await _languageDao.GetById(id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLanguage(Guid id)
        {
            await _languageDao.DeleteLanguage(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateLanguage(Guid id, [FromBody] LanguageModel language)
        {
            await _languageDao.UpdateLanguage(new Language(id, language.name, language.createdAt));
            return NoContent();
        }
    }
}
