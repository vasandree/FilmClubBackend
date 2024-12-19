using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using UserService.Application.Mappers;
using UserService.Application.Services.JwtService;
using UserService.Application.Services.RedisDbService;

namespace UserService.Application.Configurators;

public static class UserServiceApplicationConfiguration
{
    public static void ConfigureUserServiceApplication(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        builder.Services.AddAutoMapper(typeof(MappingProfile));
        builder.Services.AddScoped<IJwtService, JwtService>();
        builder.Services.AddScoped<IRedisSessionService, RedisSessionService>();

    }
}