using Slush.Data.Entity.Community.GameGroup;
using Slush.Data;
using Microsoft.EntityFrameworkCore;
using Slush.Data.Entity;

namespace Slush.DAO.GameGroupDao
{
    public class GameNewsDao
    {
        private readonly DataContext _context;

        public GameNewsDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<GameNews>> GetAllGameNews()
        {
            return await _context.dbGameNews
                .Where(g => g.deleteAt == null)
                .Select(g => new GameNews{  id = g.id,
                title = g.title,
                description = g.description,
                likesCount = g.likesCount,
                gameId = g.gameId,
                authorId = g.authorId,
                content = g.content,
                createdAt = g.createdAt}).ToListAsync();
        }

        public async Task<GameNews> UpdateGameNews(GameNews news)
        {
            var existing = await _context.dbGameNews.FindAsync(news.id);
            if (existing != null)
            {
                existing.title = news.title;
                existing.description = news.description;
                existing.likesCount = news.likesCount;
                existing.gameId = news.gameId;
                existing.authorId = news.authorId;
                existing.content = news.content;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(GameNews news)
        {
            await _context.dbGameNews.AddAsync(news);
            _context.SaveChanges();
        }

        public async Task DeleteGameNews(Guid id)
        {
            var requirement = await _context.dbGameNews.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GameNews?> GetById(Guid id)
        {
            var response = await _context.dbGameNews
                .Where(x => x.id == id)
                .Select(g => new GameNews
                {
                    id = g.id,
                    title = g.title,
                    description = g.description,
                    likesCount = g.likesCount,
                    gameId = g.gameId,
                    authorId = g.authorId,
                    content = g.content,
                    createdAt = g.createdAt
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

        public async Task<List<GameNews?>> GetByGameId(Guid id)
        {
            var response = await _context.dbGameNews
                .Where(x => x.id == id)
                .Select(g => new GameNews
                {
                    id = g.id,
                    title = g.title,
                    description = g.description,
                    likesCount = g.likesCount,
                    gameId = g.gameId,
                    authorId = g.authorId,
                    content = g.content,
                    createdAt = g.createdAt
                }).ToListAsync();
            if (response != null)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<GameNews?>> GetByIds(List<Guid> id)
        {
            List<GameNews> response = new List<GameNews> ();

            foreach(var item in id)
            {
                var result = await _context.dbGameNews
                .Where(x => x.id == item)
                .Select(g => new GameNews
                {
                    id = g.id,
                    title = g.title,
                    description = g.description,
                    likesCount = g.likesCount,
                    gameId = g.gameId,
                    authorId = g.authorId,
                    content = g.content,
                    createdAt = g.createdAt
                }).FirstOrDefaultAsync();

                if(result != null) { response.Add(result); }
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
