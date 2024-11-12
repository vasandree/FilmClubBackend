using Common.Infrastructure.GenericRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UserService.Domain.Interfaces;
using UserService.Persistence.Repositories;

namespace UserService.Persistence.Configurations;

public static class RepositoriesConfiguration
{
    public static void ConfigureRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
        builder.Services.AddTransient<IRoleRepository, RoleRepository>();
        builder.Services.AddTransient<IUserRoleRepository, UserRoleRepository>();
        builder.Services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
    }
}