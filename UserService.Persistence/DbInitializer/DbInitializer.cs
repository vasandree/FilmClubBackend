using Common.Models.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserService.Domain.Entities;
using UserService.Domain.Enums;

namespace UserService.Persistence.DbInitializer;

public class DbInitializer : IDbInitializer
{
    private readonly IConfiguration _configuration;
    private readonly UserDbContext _context;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public DbInitializer(IConfiguration configuration, UserDbContext context, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _configuration = configuration;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void Initialize()
    {
        try
        {
            if (_context.Database.GetPendingMigrations().Any())
                _context.Database.Migrate();
        }
        catch (Exception)
        {
            //todo: add logging
        }

        InitializeRoles();
        InitializeAdmin();
    }

    private void InitializeAdmin()
    {
        var adminConfig = _configuration.GetSection("AdminConfig").Get<AdminConfig>();
        var existingAdmin = _userManager.FindByEmailAsync(adminConfig.Email).GetAwaiter().GetResult();

        if (existingAdmin != null) return;

        var adminUser = new ApplicationUser
        {
            FullName = adminConfig.FullName,
            Email = adminConfig.Email,
            UserName = adminConfig.UserName
        };
        var createUserResult = _userManager.CreateAsync(adminUser, adminConfig.Password).GetAwaiter().GetResult();
        if (createUserResult.Succeeded)
        {
            _userManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
        }
        else
        {
            foreach (var error in createUserResult.Errors)
                Console.WriteLine($"Error creating admin user: {error.Description}");
            //todo: add logging
        }
    }

    private void InitializeRoles()
    {
        foreach (var role in Enum.GetValues(typeof(Role)).Cast<Role>())
        {
            var roleName = role.ToString();

            if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole<Guid>(roleName)).GetAwaiter().GetResult();
            }
        }
    }
}