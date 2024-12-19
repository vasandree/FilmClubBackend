using Common.Persistence.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces;

public interface IApplicationUserRepository : IGenericRepository<ApplicationUser>
{
    Task<ApplicationUser?> GetByIdAsync(Guid id);
    Task AddUser(ApplicationUser user, string password);
    Task<bool> ExistsAsync(Guid id);
    Task SoftDeleteAsync(ApplicationUser entity);
    public Task<ApplicationUser?> GetByEmailAsync(string email);
    public Task<ApplicationUser?> GetByUserNameAsync(string userName);
    public Task<bool> EmailExistsAsync(string email);
    public Task<bool> UserNameExistsAsync(string userName);
    public Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string password);
    public Task ChangePasswordAsync(ApplicationUser applicationUser, string oldPassword, string newPassword);
    public Task AddRole(ApplicationUser user, string role);
    public Task RemoveRole(ApplicationUser user, string role);
    public Task<bool> CheckRoleAsync(ApplicationUser user, string role);
    public Task<IList<string>> GetRolesAsync(ApplicationUser user);
}