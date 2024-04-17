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
            return await _context.dbLanguagesInGame.Select(l => new LanguageInGame
            {
                id = l.id,
                gameId = l.gameId,
                languageId = l.languageId,
                createdAt = l.createdAt
            }).ToListAsync();

        }

        public void Add(LanguageInGame language)
        {
            _context.dbLanguagesInGame.Add(language);
            _context.SaveChanges();
        }
    }
}
