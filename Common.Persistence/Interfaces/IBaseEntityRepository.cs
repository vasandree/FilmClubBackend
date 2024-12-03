using Common.Models;

namespace Common.Persistence.Interfaces;

public interface IBaseEntityRepository : IGenericRepository<BaseEntity>
{
    Task<BaseEntity?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task SoftDeleteAsync(BaseEntity entity);
}