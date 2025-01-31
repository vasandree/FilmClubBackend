using Common.Persistence.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces;

public interface IUserSettingsRepository : IGenericRepository<UserSettings>
{
    public Task<UserSettings?> GetByUserIdAsync(Guid userId);
}