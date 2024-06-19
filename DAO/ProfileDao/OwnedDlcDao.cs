using Microsoft.EntityFrameworkCore;
using Slush.Data;
using Slush.Entity.Profile;

namespace Slush.DAO.ProfileDao
{
    public class OwnedDlcDao
    {
        private readonly DataContext _dataContext;

        public OwnedDlcDao(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<OwnedDlc>> GetAllDlcs()
        {
            return await _dataContext.dbOwnedDlcs
                .Where(o => o.deleteAt == null)
                .Select(o => new OwnedDlc
                {
                    id = o.id,
                    ownedDlcId = o.ownedDlcId,
                    userId = o.userId,
                    createdAt = o.createdAt
                }).ToListAsync();
        }

        public async Task<OwnedDlc> UpdateOwned(OwnedDlc dlc)
        {
            var existing = await _dataContext.dbOwnedDlcs.FindAsync(dlc.id);

            if(existing != null)
            {
                existing.ownedDlcId = dlc.id;
                existing.userId = dlc.userId;

                await _dataContext.SaveChangesAsync();
            }

            return existing;
        }

        public async Task Add(OwnedDlc dlc)
        {
            await _dataContext.dbOwnedDlcs.AddAsync(dlc);
            await _dataContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var existing = await _dataContext.dbOwnedDlcs.FindAsync(id);
            
            if(existing != null )
            {
                existing.deleteAt = DateTime.Now;
                
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<OwnedDlc?> GetById(Guid id)
        {
            var response = await _dataContext.dbOwnedDlcs
                .Where(d => d.id == id)
                .Select(d => new OwnedDlc
                {
                    id = d.id,
                    ownedDlcId = d.id,
                    userId = d.userId,
                    createdAt = d.createdAt
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
        public async Task<List<OwnedDlc?>> GetByUserId(Guid id)
        {
            var response = await _dataContext.dbOwnedDlcs
                .Where(d => d.userId == id)
                .Select(d => new OwnedDlc
                {
                    id = d.id,
                    ownedDlcId = d.id,
                    userId = d.userId,
                    createdAt = d.createdAt
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

        public async Task<List<OwnedDlc>> GetByIds(List<Guid> id)
        {
            List<OwnedDlc> response = new List<OwnedDlc> ();

            foreach(var item in id)
            {
                var result = await _dataContext.dbOwnedDlcs
                .Where(d => d.userId == item)
                .Select(d => new OwnedDlc
                {
                    id = d.id,
                    ownedDlcId = d.id,
                    userId = d.userId,
                    createdAt = d.createdAt
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
