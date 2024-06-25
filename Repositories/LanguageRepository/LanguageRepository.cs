using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity;

namespace Slush.DAO.LanguageRepository
{
    public class LanguageRepository
    {
        private readonly DataContext _context;

        public LanguageRepository(DataContext context)
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

        public async Task<Language> UpdateLanguage(Language language)
        {
            var existing = await _context.dbLanguages.FindAsync(language.id);
            if (existing != null)
            {
                existing.name = language.name;

                await _context.SaveChangesAsync();
            }

            return existing;
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

        public async Task<List<Language?>> GetByIds(List<Guid> id)
        {
            List<Language> response = new List<Language> ();

            foreach(var item in id)
            {
                var result = await _context.dbLanguages
                .Where(x => x.id == item)
                .Select(l => new Language
                {
                    id = l.id,
                    name = l.name,
                    createdAt = l.createdAt
                }).FirstOrDefaultAsync();

                if(result != null)
                {
                    response.Add(result);
                }
            }
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
