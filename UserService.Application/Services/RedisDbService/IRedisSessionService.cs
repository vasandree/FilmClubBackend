namespace UserService.Application.Services.RedisDbService;

public interface IRedisSessionService
{
    public Task StoreTokenAsync(Guid userId, string jwtToken);
    public Task<string?> GetTokenAsync(Guid userId);
    public Task DeleteTokenAsync(Guid userId);
}