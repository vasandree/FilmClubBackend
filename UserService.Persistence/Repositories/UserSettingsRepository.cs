using Common.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Persistence.Repositories;

public class UserSettingsRepository : GenericRepository<UserSettings>, IUserSettingsRepository
{
    private readonly UserDbContext _context;
    
    public UserSettingsRepository(UserDbContext context, UserDbContext context1) : base(context)
    {
        _context = context1;
    }

    public async Task<UserSettings?> GetByUserIdAsync(Guid userId)
    {
        return await _context.UserSettings.FirstOrDefaultAsync(x => x.UserId == userId);
    }
}