using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces;

public interface IUserRoleRepository
{
    public Task AddUserRoleAsync(UserRole userRole);
    public Task RemoveUserRoleAsync(UserRole userRole);
    public Task<List<UserRole>> GetUserRolesByUserIdAsync(Guid userId);
    public Task<bool> IsUserInRoleAsync(Guid userId, Guid roleId);
}