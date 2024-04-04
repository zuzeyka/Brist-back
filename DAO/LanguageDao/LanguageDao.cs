using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Slush.Data;
using Slush.Data.Entity;

namespace Slush.DAO.LanguageDao
{
    public class LanguageDao
    {
        private readonly DataContext _context;

        public LanguageDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Language>> GetAllLanguages()
        {
            var _languageEntities = await _context.dbLanguages.AsNoTracking().ToListAsync();

            var _languages = _languageEntities.Select(l => new Language(l.id,
                                                                        l.name)).ToList();
            return _languages;
        }

        public void Add(Language language)
        {
            _context.dbLanguages.Add(language);
            _context.SaveChanges();
        }
    }
}
