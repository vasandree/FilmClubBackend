using Common.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Persistence.Repositories;

public class ApplicationUserRepository : GenericRepository<ApplicationUser>, IApplicationUserRepository
{
    private readonly UserDbContext _context;


    public ApplicationUserRepository(UserDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ApplicationUser?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<bool> ExistsAsync(Guid id)
    {
        return _context.Users.AnyAsync(u => u.Id == id);
    }

    public async Task SoftDeleteAsync(ApplicationUser entity)
    {
        entity.IsDeleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task<ApplicationUser?> GetByEmailAsync(string email)
    {
        return await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<ApplicationUser?> GetByUserNameAsync(string userName)
    {
        return await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Username == userName);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.ApplicationUsers.AnyAsync(u => u.Email == email);
    }

    public Task<bool> UserNameExistsAsync(string userName)
    {
        return _context.ApplicationUsers.AnyAsync(u => u.Username == userName);
    }
}