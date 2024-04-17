using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Slush.Data;
using Slush.Data.Entity;
using Slush.Entity.Store.Product.Creators;

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
            return await _context.dbLanguages.Select(l => new Language {
                id = l.id,
                name = l.name,
                createdAt = l.createdAt}).ToListAsync();
        }
        public void Add(Language language)
        {
            _context.dbLanguages.Add(language);
            _context.SaveChanges();
        }
    }
}
