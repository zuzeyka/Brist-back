using Slush.Entity.Profile;

namespace Slush.Repositories.IRepository
{
    public interface IOwnedDlcRepository
    {
        Task<List<OwnedDlc>> GetAllDlcs();
        Task<OwnedDlc> UpdateOwned(OwnedDlc dlc);
        Task Add(OwnedDlc dlc);
        Task Delete(Guid id);
        Task<OwnedDlc?> GetById(Guid id);
        Task<List<OwnedDlc?>> GetByUserId(Guid id);
        Task<List<OwnedDlc>> GetByIds(List<Guid> id);
    }
}
