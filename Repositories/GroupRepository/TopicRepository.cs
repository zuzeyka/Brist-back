using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Data.Entity.Community;
using Slush.Repositories.IRepository;

namespace Slush.Repositories.GroupRepository
{
    public class TopicRepository : ITopicRepository
    {
        private readonly DataContext _context;

        public TopicRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Topic>> GetAllTopics()
        {
            return await _context.dbTopics
                .Where(t => t.deleteAt == null)
                .Select(t => new Topic {
                id = t.id,
                attachedId = t.attachedId,
                name = t.name,
                description = t.description,
                authorId = t.authorId,
                createdAt = t.createdAt}).ToListAsync();
        }
        public async Task<Topic> UpdateTopic(Topic topic)
        {
            var existing = await _context.dbTopics.FindAsync(topic.id);
            if (existing != null)
            {
                existing.name = topic.name;
                existing.description = topic.description;

                await _context.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(Topic topic)
        {
            await _context.dbTopics.AddAsync(topic);
            _context.SaveChanges();
        }

        public async Task DeleteTopic(Guid id)
        {
            var requirement = await _context.dbTopics.FindAsync(id);
            if (requirement != null)
            {
                requirement.deleteAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Topic?> GetById(Guid id)
        {
            var response = await _context.dbTopics
                .Where(x => x.id == id)
                .Select(t => new Topic
                {
                    id = t.id,
                    attachedId = t.attachedId,
                    name = t.name,
                    description = t.description,
                    authorId = t.authorId,
                    createdAt = t.createdAt
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

        public async Task<List<Topic?>> GetByAttachedId(Guid id)
        {
            var response = await _context.dbTopics
                .Where(x => x.attachedId == id)
                .Where(c => c.deleteAt == null)
                .Select(t => new Topic
                {
                    id = t.id,
                    attachedId = t.attachedId,
                    name = t.name,
                    description = t.description,
                    authorId = t.authorId,
                    createdAt = t.createdAt
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

        public async Task<List<Topic?>> GetByIds(List<Guid> ids)
        {
            List<Topic> response = new List<Topic> ();

            foreach(var id in ids)
            {
                var result = await _context.dbTopics
                    .Where(x => x.id == id)
                    .Where(c => c.deleteAt == null)
                    .Select(t => new Topic
                    {
                        id = t.id,
                        attachedId = t.attachedId,
                        name = t.name,
                        description = t.description,
                        authorId = t.authorId,
                        createdAt = t.createdAt
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
