using Common.Services.RedisDbService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Configurations.Filters;

public class ValidateSessionAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var redisSessionService = context.HttpContext.RequestServices.GetService<IRedisSessionService>();

        if (redisSessionService == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userIdClaim = context.HttpContext.User.FindFirst("UserId")?.Value;
        var sessionIdClaim = context.HttpContext.User.FindFirst("SessionId")?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(sessionIdClaim))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        try
        {
            var storedSession = await redisSessionService.GetSessionTokenAsync(userId, sessionIdClaim);
            if (string.IsNullOrEmpty(storedSession))
            {
                context.Result = new UnauthorizedResult();
            }
        }
        catch (Exception ex)
        {
            //todo: add logger
            context.Result = new UnauthorizedResult();
        }
    }
}
