using Slush.Data.Entity;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity.Community.GameGroup;

namespace Slush.Repositories.LanguageRepository
{
    public class LanguageInGameRepository
    {
        private readonly DataContext _context;

        public LanguageInGameRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<LanguageInGame>> GetAllLanguageInGames()
        {
            return await _context.dbLanguagesInGame
                .Where(l => l.deleteAt == null)
                .Select(l => new LanguageInGame
            {
                id = l.id,
                gameId = l.gameId,
                languageId = l.languageId,
                createdAt = l.createdAt
            }).ToListAsync();

        }

        public async Task<LanguageInGame> UpdateLanguageInGame(LanguageInGame language)
        {
            var existing = await _context.dbLanguagesInGame.FindAsync(language.id);
            if (existing != null)
            {
                existing.languageId = language.id;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(LanguageInGame language)
        {
            await _context.dbLanguagesInGame.AddAsync(language);
            _context.SaveChanges();
        }

        public async Task DeleteLanguageInGame(Guid id)
        {
            var requirement = await _context.dbLanguagesInGame.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<LanguageInGame?> GetById(Guid id)
        {
            var response = await _context.dbLanguagesInGame
                .Where(x => x.id == id)
                .Select(l => new LanguageInGame
                {
                    id = l.id,
                    gameId = l.gameId,
                    languageId = l.languageId,
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

        public async Task<List<LanguageInGame?>> GetByIds(List<Guid> id)
        {
            List<LanguageInGame> response = new List<LanguageInGame>();

            foreach(var item in id)
            {
                var result = await _context.dbLanguagesInGame
                .Where(x => x.id == item)
                .Select(l => new LanguageInGame
                {
                    id = l.id,
                    gameId = l.gameId,
                    languageId = l.languageId,
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
