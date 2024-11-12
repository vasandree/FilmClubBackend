using Common.Infrastructure.GenericRepository;
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

    public async Task<ApplicationUser?> GetByEmailAsync(string email)
    {
        return await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<ApplicationUser?> GetByUserNameAsync(string userName)
    {
        return await _context.ApplicationUsers.FirstOrDefaultAsync(u=> u.Username == userName);
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