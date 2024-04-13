using FullStackBrist.Server.Models.Language;
using Microsoft.AspNetCore.Mvc;
using Slush.DAO.LanguageDao;
using Slush.Data;
using Slush.Data.Entity;

namespace FullStackBrist.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                                                                                         l.languageId,
                                                                                         l.createdAt)).ToList();
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<LanguageInGame>> CreateLanguageInGame([FromBody] LanguageInGame model)
        {
            var result = new LanguageInGame(Guid.NewGuid(),
                                            model.gameId,
                                            model.languageId,
                                            DateTime.Now
                                            );
            _dataContext.dbLanguagesInGame.AddAsync(result);

            return result;
        }
    }
}
