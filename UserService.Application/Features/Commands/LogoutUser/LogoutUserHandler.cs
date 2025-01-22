using Common.Models.Models.Exceptions;
using Common.Services.RedisDbService;
using MediatR;
using UserService.Application.Helpers;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Commands.LogoutUser;

public class LogoutUserHandler : IRequestHandler<LogoutUserCommand, Unit>
{
    private readonly IChecker _checker;
    private readonly IRedisSessionService _redisSessionService;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public LogoutUserHandler(IRedisSessionService redisSessionService,
        IApplicationUserRepository applicationUserRepository, IRefreshTokenRepository refreshTokenRepository,
        IChecker checker)
    {
        _redisSessionService = redisSessionService;
        _applicationUserRepository = applicationUserRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _checker = checker;
    }

    public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        await _checker.CheckUserExistsAsync(request.UserId);

        await _checker.CheckRefreshTokenExistsAsync(request.RefreshToken);

        await _refreshTokenRepository.DeleteByStringAsync(request.RefreshToken);

        await _redisSessionService.DeleteSessionAsync(request.UserId, request.SessionId);
        return Unit.Value;
    }
}