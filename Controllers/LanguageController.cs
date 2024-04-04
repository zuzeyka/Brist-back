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
                                                                        l.name)).ToList();

            return Ok(response);
        }
    }
}
