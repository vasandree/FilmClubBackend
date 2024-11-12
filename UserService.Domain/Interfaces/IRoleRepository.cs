using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces;

public interface IRoleRepository
{
    public Task<Role?> GetRoleByIdAsync(Guid roleId);
    public Task<Role?> GetRoleByNameAsync(string name);
    public Task<List<Role?>> GetRolesAsync();
}