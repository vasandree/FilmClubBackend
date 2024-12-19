using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using UserService.Persistence.DbInitializer;

namespace UserService.Persistence.Configurations;

public static class DbContextConfiguration
{
    public static void ConfigureUserDb(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<UserDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("UserDb")));

        builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")!));
        
        builder.Services.AddScoped<IDbInitializer, DbInitializer.DbInitializer>();

    }

    public static async Task ConfigureUserDb(this WebApplication application)
    {
        using var scope = application.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();

        try
        {
            await dbContext.Database.MigrateAsync();
            var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await initializer.InitializeAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Migration failed: {ex.Message}");
        }
    }
}