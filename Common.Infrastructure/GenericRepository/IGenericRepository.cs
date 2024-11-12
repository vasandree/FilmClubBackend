using System.Linq.Expressions;
using Common.Models;

namespace Common.Infrastructure.GenericRepository;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>?> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<bool> ExistsAsync(Guid id);
    Task SoftDeleteAsync(T entity);
}