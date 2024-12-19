using Common.Models;

namespace Common.Persistence.Interfaces;

public interface IBaseEntityRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    Task<BaseEntity?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task SoftDeleteAsync(BaseEntity entity);
}