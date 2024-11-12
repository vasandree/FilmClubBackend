using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Persistence.Repositories;

public class RoleRepository: IRoleRepository
{
    private readonly UserDbContext _context;

    public RoleRepository(UserDbContext context)
    {
        _context = context;
    }


    public async Task<Role?> GetRoleByIdAsync(Guid roleId)
    {
        return await _context.Roles.FirstOrDefaultAsync(x=>x.Id == roleId);
    }

    public async Task<Role?> GetRoleByNameAsync(string name)
    {
        var roleEnum = (Domain.Enums.Role)Enum.Parse(typeof(Domain.Enums.Role), name);
        return await _context.Roles.FirstOrDefaultAsync(x=>x.RoleName == roleEnum);
    }

    public async Task<List<Role?>> GetRolesAsync()
    {
        return await _context.Roles.ToListAsync();
    }
}