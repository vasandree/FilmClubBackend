using Common.Infrastructure.GenericRepository;
using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces;

public interface IApplicationUserRepository : IGenericRepository<ApplicationUser>
{
    public Task<ApplicationUser?> GetByEmailAsync(string email);
    public Task<ApplicationUser?> GetByUserNameAsync(string userName);
    public Task<bool> EmailExistsAsync(string email);
    public Task<bool> UserNameExistsAsync(string userName);
}