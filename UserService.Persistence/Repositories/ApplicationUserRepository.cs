using Common.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Persistence.Repositories;

public class ApplicationUserRepository : GenericRepository<ApplicationUser>, IApplicationUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly UserDbContext _context;


    public ApplicationUserRepository(UserDbContext context, UserManager<ApplicationUser> userManager,
        UserDbContext dbContext) : base(context)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<ApplicationUser?> GetByIdAsync(Guid id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task AddUser(ApplicationUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
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
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<ApplicationUser?> GetByUserNameAsync(string userName)
    {
        return await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.ApplicationUsers.AnyAsync(u => u.Email == email);
    }

    public Task<bool> UserNameExistsAsync(string userName)
    {
        return _context.ApplicationUsers.AnyAsync(u => u.UserName == userName);
    }

    public async Task<bool> CheckPasswordAsync(ApplicationUser applicationUser, string password)
    {
        return await _userManager.CheckPasswordAsync(applicationUser, password);
    }

    public async Task ChangePasswordAsync(ApplicationUser applicationUser, string oldPassword, string newPassword)
    {
        await _userManager.ChangePasswordAsync(applicationUser, oldPassword, newPassword);
    }
    
    public async Task AddRole(ApplicationUser user, string role)
    {
        await _userManager.AddToRoleAsync(user, role);
    }

    public async Task RemoveRole(ApplicationUser user, string role)
    {
        await _userManager.RemoveFromRoleAsync(user, role);
    }

    public async Task<bool> CheckRoleAsync(ApplicationUser user, string role)
    {
        return await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }
}