using Common.Persistence.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Common.Configurations.Configurations;

public static class MiddlewareConfiguration
{
    public static void UseMiddleware(this WebApplication app)
    {
        app.UseMiddleware<MiddlewareService>();
    }
}