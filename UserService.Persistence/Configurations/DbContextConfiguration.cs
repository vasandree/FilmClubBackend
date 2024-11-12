
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserService.Persistence.Configurations;

public static class DbContextConfiguration
{
    public static void ConfigureUserDb(this WebApplicationBuilder builder)
    {
        var connection = builder.Configuration.GetConnectionString("PostgresUser");
        builder.Services.AddDbContext<UserDbContext>(options =>
            options.UseNpgsql(connection));
    }

    public static void ConfigureUserDb(this WebApplication application)
    {
        using (var scope = application.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();

            try
            {
                dbContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Migration failed: {ex.Message}");
            }
        }
    }
}