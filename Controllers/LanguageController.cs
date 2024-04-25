using FullStackBrist.Server.Models.Group;
using FullStackBrist.Server.Models.Language;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.GroupDao;
using Slush.DAO.LanguageDao;
using Slush.Data;
using Slush.Data.Entity;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LanguageController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly LanguageDao _languageDao;

        public LanguageController(DataContext dataContext, LanguageDao languageDao)
        {
            _dataContext = dataContext;
            _languageDao = languageDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<LanguageDao>>> GetAllLanguages()
        {
            var _languages = await _languageDao.GetAllLanguages();

            var response = _languages.Select(l => new Language(id: l.id,
                                                               name: l.name,
                                                               createdAt: l.createdAt)).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Language>> CreateLanguage([FromBody] LanguageModel model)
        {
            var result = new Language(Guid.NewGuid(),
                                            model.name,
                                            DateTime.Now
                                            );
            var response = _dataContext.dbLanguages.AddAsync(result);

            return Ok(response);
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
            var result = new Language(id, language.name, language.createdAt);
            await _languageDao.UpdateLanguage(result);
            return NoContent();
        }
    }
}
