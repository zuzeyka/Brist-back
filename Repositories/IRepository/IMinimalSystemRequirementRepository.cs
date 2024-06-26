using Slush.Data.Entity;

namespace Slush.Repositories.IRepository
{
    public interface IMinimalSystemRequirementRepository
    {
        Task<List<MinimalSystemRequirement>> GetAllMinimalSystemRequirements();
        Task<MinimalSystemRequirement> UpdateMinimalSystemRequirement(MinimalSystemRequirement requirement);
        Task Add(MinimalSystemRequirement systemRequirements);
        Task DeleteMinimalSystemRequirement(Guid id);
        Task<MinimalSystemRequirement?> GetById(Guid id);
        Task<List<MinimalSystemRequirement?>> GetByGameId(Guid id);
        Task<List<MinimalSystemRequirement?>> GetByIds(List<Guid> id);

    }
}
