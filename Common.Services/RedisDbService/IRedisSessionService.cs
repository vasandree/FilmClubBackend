namespace Common.Services.RedisDbService;
public interface IRedisSessionService
{
    Task StoreSessionAsync(Guid userId, string sessionId, string jwtToken);
    Task<string?> GetSessionTokenAsync(Guid userId, string sessionId);
    Task DeleteSessionAsync(Guid userId, string sessionId);
    Task DeleteAllSessionsAsync(Guid userId);
}
