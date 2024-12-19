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

    public async Task InitializeAsync()
    {
        try
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            // TODO: Add proper logging here
            Console.WriteLine($"Error during migration: {ex.Message}");
        }

        await InitializeRolesAsync();
        await InitializeAdminAsync();
    }

    private async Task InitializeAdminAsync()
    {
        var adminConfig = _configuration.GetSection("AdminConfig").Get<AdminConfig>();
        if (adminConfig == null)
        {
            Console.WriteLine("Admin configuration is missing.");
            return;
        }

        var existingAdmin = await _userManager.FindByEmailAsync(adminConfig.Email);

        if (existingAdmin != null) return;

        var adminUser = new ApplicationUser
        {
            FullName = adminConfig.FullName,
            Email = adminConfig.Email,
            UserName = adminConfig.UserName
        };

        var createUserResult = await _userManager.CreateAsync(adminUser, adminConfig.Password);

        if (createUserResult.Succeeded)
        {
            var addToRoleResult = await _userManager.AddToRoleAsync(adminUser, "Admin");
            if (!addToRoleResult.Succeeded)
            {
                Console.WriteLine("Failed to assign 'Admin' role to the admin user.");
                foreach (var error in addToRoleResult.Errors)
                {
                    Console.WriteLine($"Error: {error.Description}");
                }
                // TODO: Add proper logging here
            }
        }
        else
        {
            Console.WriteLine("Failed to create admin user.");
            foreach (var error in createUserResult.Errors)
            {
                Console.WriteLine($"Error: {error.Description}");
            }
            // TODO: Add proper logging here
        }
    }

    private async Task InitializeRolesAsync()
    {
        foreach (var role in Enum.GetValues(typeof(Role)).Cast<Role>())
        {
            var roleName = role.ToString();

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
                if (!result.Succeeded)
                {
                    Console.WriteLine($"Failed to create role '{roleName}'.");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error: {error.Description}");
                    }
                    // TODO: Add proper logging here
                }
            }
        }
    }
}
