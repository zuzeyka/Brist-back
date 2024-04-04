using Slush.Data.Entity;
using Slush.Data;
using Microsoft.EntityFrameworkCore;

namespace Slush.DAO.LanguageDao
{
    public class LanguageInGameDao
    {
        private readonly DataContext _context;

        public LanguageInGameDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<LanguageInGame>> GetAllLanguageInGames()
        {
            var _languageInGameEntities = await _context.dbLanguagesInGame.AsNoTracking().ToListAsync();

            var _languageInGame = _languageInGameEntities.Select(l => new LanguageInGame(l.id,
                                                                                         l.gameId,
                                                                                         l.languageId)).ToList(); 

            return _languageInGame;
        }

        public void Add(LanguageInGame language)
        {
            _context.dbLanguagesInGame.Add(language);
            _context.SaveChanges();
        }
    }
}
