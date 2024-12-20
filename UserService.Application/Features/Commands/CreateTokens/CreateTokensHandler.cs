using Common.Models.Models.Exceptions;
using Common.Services.RedisDbService;
using MediatR;
using Microsoft.Extensions.Configuration;
using UserService.Application.Dtos.Responses;
using UserService.Application.Services.JwtService;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Commands.CreateTokens;

public class CreateTokensHandler : IRequestHandler<CreateTokensCommand, TokensDto>
{
    private readonly IJwtService _jwtService;
    private readonly IConfiguration _configuration;
    private readonly IRedisSessionService _redisSessionService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IApplicationUserRepository _applicationUserRepository;

    public CreateTokensHandler(IJwtService jwtService, IConfiguration configuration,
        IRefreshTokenRepository refreshTokenRepository, IRedisSessionService redisSessionService,
        IApplicationUserRepository applicationUserRepository)
    {
        _jwtService = jwtService;
        _configuration = configuration;
        _refreshTokenRepository = refreshTokenRepository;
        _redisSessionService = redisSessionService;
        _applicationUserRepository = applicationUserRepository;
    }

    public async Task<TokensDto> Handle(CreateTokensCommand request, CancellationToken cancellationToken)
    {
        if (!await _applicationUserRepository.ExistsAsync(request.UserId))
            throw new BadRequest($"User with id: {request.UserId} does not exist");

        var user = await _applicationUserRepository.GetByIdAsync(request.UserId);

        var sessionId = Guid.NewGuid().ToString();

        var authToken = _jwtService.GenerateTokenString(user.Email!, user.UserName, user.Id, sessionId);
        string? refreshToken = null;

        if (user.RememberMe)
        {
            refreshToken = _jwtService.GenerateRefreshTokenString();

            var refreshTokenEntity = new RefreshToken(user, refreshToken,
                DateTime.UtcNow.AddDays(_configuration.GetValue<int>("Jwt:RefreshDaysLifeTime")));

            await _refreshTokenRepository.CreateAsync(refreshTokenEntity);
        }

        if (request.RefreshToken != null)
        {
            if (!await _refreshTokenRepository.ExistsAsync(request.RefreshToken))
                throw new BadRequest("Provided refresh token does not exist.");

            await _refreshTokenRepository.DeleteByStringAsync(request.RefreshToken);
        }

        await _redisSessionService.StoreSessionAsync(user.Id, sessionId, authToken);

        return new TokensDto(authToken, refreshToken);
    }
}