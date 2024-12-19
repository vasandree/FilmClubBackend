using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace UserService.Application.Services.RedisDbService;

public class RedisSessionService : IRedisSessionService
{
    private readonly IDatabase _redisDb;
    private readonly IConfiguration _configuration;

    public RedisSessionService(IConnectionMultiplexer connectionMultiplexer, IConfiguration configuration)
    {
        _configuration = configuration;
        _redisDb = connectionMultiplexer.GetDatabase();
    }

    public async Task StoreTokenAsync(Guid userId, string jwtToken)
    {
        var key = $"session:{userId}";
        var expiry = TimeSpan.FromMinutes(_configuration.GetValue<int>("Jwt:AccessMinutesLifeTime"));
        await _redisDb.StringSetAsync(key, jwtToken, expiry);
    }

    public async Task<string?> GetTokenAsync(Guid userId)
    {
        var key = $"session:{userId}";
        return await _redisDb.StringGetAsync(key);
    }

    public async Task DeleteTokenAsync(Guid userId)
    {
        var key = $"session:{userId}";
        await _redisDb.KeyDeleteAsync(key);
    }
}