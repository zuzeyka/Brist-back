using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Slush.Data;
using Slush.Data.Entity;
using Slush.Data.Entity.Community.GameGroup;
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
            return await _context.dbLanguages
                .Where(l => l.deleteAt == null)
                .Select(l => new Language {
                id = l.id,
                name = l.name,
                createdAt = l.createdAt}).ToListAsync();
        }

        public async Task UpdateLanguage(Language language)
        {
            var existing = await _context.dbLanguages.FindAsync(language.id);
            if (existing != null)
            {
                existing.name = language.name;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Add(Language language)
        {
            await _context.dbLanguages.AddAsync(language);
            _context.SaveChanges();
        }

        public async Task DeleteLanguage(Guid id)
        {
            var requirement = await _context.dbLanguages.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Language?> GetById(Guid id)
        {
            var response = await _context.dbLanguages
                .Where(x => x.id == id)
                .Select(l => new Language
                {
                    id = l.id,
                    name = l.name,
                    createdAt = l.createdAt
                }).FirstOrDefaultAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }
    }
}
