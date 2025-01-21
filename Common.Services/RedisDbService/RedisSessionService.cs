using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Common.Services.RedisDbService;

public class RedisSessionService : IRedisSessionService
{
    private readonly IDatabase _redisDb;
    private readonly IConfiguration _configuration;

    public RedisSessionService(IConnectionMultiplexer connectionMultiplexer, IConfiguration configuration)
    {
        _configuration = configuration;
        _redisDb = connectionMultiplexer.GetDatabase();
    }

    public async Task StoreSessionAsync(Guid userId, string sessionId, string jwtToken)
    {
        var key = $"sessions:{userId}";
        var expiry = TimeSpan.FromMinutes(_configuration.GetValue<int>("Jwt:AccessMinutesLifeTime"));

        await _redisDb.HashSetAsync(key, sessionId, jwtToken);

        await _redisDb.KeyExpireAsync(key, expiry);
    }

    public async Task<string?> GetSessionTokenAsync(Guid userId, string sessionId)
    {
        var key = $"sessions:{userId}";

        var token = await _redisDb.HashGetAsync(key, sessionId);
        return token.IsNullOrEmpty ? null : token.ToString();
    }

    public async Task DeleteSessionAsync(Guid userId, string sessionId)
    {
        var key = $"sessions:{userId}";

        await _redisDb.HashDeleteAsync(key, sessionId);
    }

    public async Task DeleteAllSessionsAsync(Guid userId)
    {
        var key = $"sessions:{userId}";

        await _redisDb.KeyDeleteAsync(key);
    }
}