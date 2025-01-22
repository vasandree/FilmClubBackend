using Common.Models.Models.Exceptions;
using Common.Services.RedisDbService;
using MediatR;
using Microsoft.Extensions.Configuration;
using UserService.Application.Dtos.Responses;
using UserService.Application.Helpers;
using UserService.Application.Services.JwtService;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Commands.CreateTokens;

public class CreateTokensHandler : IRequestHandler<CreateTokensCommand, TokensDto>
{
    private readonly IChecker _checker;
    private readonly IJwtService _jwtService;
    private readonly IConfiguration _configuration;
    private readonly IRedisSessionService _redisSessionService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IApplicationUserRepository _applicationUserRepository;

    public CreateTokensHandler(IJwtService jwtService, IConfiguration configuration,
        IRefreshTokenRepository refreshTokenRepository, IRedisSessionService redisSessionService,
        IApplicationUserRepository applicationUserRepository, IChecker checker)
    {
        _jwtService = jwtService;
        _configuration = configuration;
        _refreshTokenRepository = refreshTokenRepository;
        _redisSessionService = redisSessionService;
        _applicationUserRepository = applicationUserRepository;
        _checker = checker;
    }

    public async Task<TokensDto> Handle(CreateTokensCommand request, CancellationToken cancellationToken)
    {
        await _checker.CheckUserExistsAsync(request.UserId);

        var user = await _applicationUserRepository.GetByIdAsync(request.UserId);

        var sessionId = Guid.NewGuid().ToString();

        var authToken = _jwtService.GenerateTokenString(user.Email!, user.UserName, user.Id, sessionId);
        string? refreshToken = null;

        if (user.RememberMe)
        {
            refreshToken = _jwtService.GenerateRefreshTokenString();

            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpireTime = DateTime.UtcNow.AddDays(_configuration.GetValue<int>("Jwt:RefreshDaysLifeTime")),
                User = user
            };


            await _refreshTokenRepository.CreateAsync(refreshTokenEntity);
        }

        if (request.RefreshToken != null)
        {
            await _checker.CheckRefreshTokenExistsAsync(request.RefreshToken);

            await _refreshTokenRepository.DeleteByStringAsync(request.RefreshToken);
        }

        await _redisSessionService.StoreSessionAsync(user.Id, sessionId, authToken);

        return new TokensDto(authToken, refreshToken);
    }
}