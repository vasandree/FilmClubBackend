using Common.Models;
using Common.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Common.Persistence.Repositories;

public class BaseEntityRepository<T>: GenericRepository<T>, IBaseEntityRepository<T> where T : BaseEntity
{
    private readonly DbContext _context;
    private readonly DbSet<BaseEntity> _dbSet;
    
    public BaseEntityRepository(DbContext context) : base(context)
    {
        _context = context;
        _dbSet = context.Set<BaseEntity>();
    }

    public async Task<BaseEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
       return await _dbSet.AnyAsync(x => x.Id == id);
    }

    public async Task SoftDeleteAsync(BaseEntity entity)
    {
        entity.IsDeleted = true;
        await _context.SaveChangesAsync();
    }
}