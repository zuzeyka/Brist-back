using Microsoft.AspNetCore.Mvc;
using Slush.DAO.LanguageDao;
using Slush.Data;
using Slush.Data.Entity;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("[languageInGameController]")]
    public class LanguageInGameController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly LanguageInGameDao _languageInGameDao;

        public LanguageInGameController(DataContext dataContext, LanguageInGameDao languageInGameDao)
        {
            _dataContext = dataContext;
            _languageInGameDao = languageInGameDao;
        }

        [HttpGet]
        public async Task<ActionResult<List<LanguageInGameDao>>> GetAllLanguagesInGame()
        {
            var _languages = await _languageInGameDao.GetAllLanguageInGames();

            var response = _languages.Select(l => new LanguageInGame(l.id,
                                                                                         l.gameId,
                                                                                         l.languageId)).ToList();
            return Ok(response);
        }
    }
}
