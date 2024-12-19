using System.Security.Claims;

namespace UserService.Application.Services.JwtService;

public interface IJwtService
{
    string GenerateTokenString(string email, string username, Guid id);
    ClaimsPrincipal? GetTokenPrincipal(string token);
    string? GenerateRefreshTokenString();

}