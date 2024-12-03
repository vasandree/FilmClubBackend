using Common.Persistence.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces;

public interface IApplicationUserRepository: IGenericRepository<ApplicationUser>
{
    Task<ApplicationUser?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task SoftDeleteAsync(ApplicationUser entity);
    public Task<ApplicationUser?> GetByEmailAsync(string email);
    public Task<ApplicationUser?> GetByUserNameAsync(string userName);
    public Task<bool> EmailExistsAsync(string email);
    public Task<bool> UserNameExistsAsync(string userName);
    
}