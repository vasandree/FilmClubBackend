using System.Reflection;
using Common.Services.RedisDbService;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using UserService.Application.Helpers;
using UserService.Application.Mappers;
using UserService.Application.Services.JwtService;

namespace UserService.Application.Configurators;

public static class UserServiceApplicationConfiguration
{
    public static void ConfigureUserServiceApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Services.AddScoped<IJwtService, JwtService>();
        builder.Services.AddScoped<IRedisSessionService, RedisSessionService>();
        builder.Services.AddScoped<IChecker, Checker>();
    }
}