using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Persistence.Repositories;

public class UserRoleRepository: IUserRoleRepository
{
    private readonly UserDbContext _context;

    public UserRoleRepository(UserDbContext context)
    {
        _context = context;
    }

    public async Task AddUserRoleAsync(UserRole userRole)
    {
       await _context.UserRoles.AddAsync(userRole);
       await _context.SaveChangesAsync();
    }

    public async Task RemoveUserRoleAsync(UserRole userRole)
    {
        _context.UserRoles.Remove(userRole);
        await _context.SaveChangesAsync();
    }

    public async Task<List<UserRole>> GetUserRolesByUserIdAsync(Guid userId)
    {
        return await _context.UserRoles.Where(x=>x.UserId == userId).ToListAsync();
    }

    public async Task<bool> IsUserInRoleAsync(Guid userId, Guid roleId)
    {
        return await _context.UserRoles.AnyAsync(x => x.UserId == userId && x.RoleId == roleId);
    }
}