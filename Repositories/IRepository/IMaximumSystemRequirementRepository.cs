using Slush.Data.Entity;

namespace Slush.Repositories.IRepository
{
    public interface IMaximumSystemRequirementRepository
    {
        Task<List<MaximumSystemRequirement>> GetAllMaximumSystemRequirements();
        Task<MaximumSystemRequirement> UpdateMaximumSystemRequirement(MaximumSystemRequirement requirement);
        Task Add(MaximumSystemRequirement systemRequirements);
        Task DeleteMaximumSystemRequirement(Guid id);
        Task<MaximumSystemRequirement?> GetById(Guid id);
        Task<List<MaximumSystemRequirement?>> GetByIds(List<Guid> id);
        Task<List<MaximumSystemRequirement?>> GetByGameName(Guid id);
    }
}
