using FullStackBrist.Server.Models.Group;
using FullStackBrist.Server.Models.Language;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.LanguageDao;
using Slush.Data;
using Slush.Data.Entity;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[languageController]")]
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

            var response = _languages.Select(l => new Language(l.id,
                                                                        l.name,
                                                                        l.createdAt)).ToList();

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Language>> CreateLanguage([FromBody] LanguageModel model)
        {
            var result = new Language(Guid.NewGuid(),
                                            model.name,
                                            DateTime.Now
                                            );
            _dataContext.dbLanguages.AddAsync(result);

            return result;
        }
    }
}
