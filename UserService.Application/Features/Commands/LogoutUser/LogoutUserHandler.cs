using Common.Models.Models.Exceptions;
using MediatR;
using UserService.Application.Services.RedisDbService;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Commands.LogoutUser;

public class LogoutUserHandler: IRequestHandler<LogoutUserCommand, Unit>
{
    private readonly IRedisSessionService _redisSessionService;
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    
    public LogoutUserHandler(IRedisSessionService redisSessionService, IApplicationUserRepository applicationUserRepository, IRefreshTokenRepository refreshTokenRepository)
    {
        _redisSessionService = redisSessionService;
        _applicationUserRepository = applicationUserRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
    {
        if(!await _applicationUserRepository.ExistsAsync(request.UserId))
            throw new BadRequest($"User with id={request.UserId} does not exist");
        
        if(!await _refreshTokenRepository.ExistsAsync(request.RefreshToken))
            throw new BadRequest("Provided refresh token does not exist");
        
        await _refreshTokenRepository.DeleteByStringAsync(request.RefreshToken);
        
        await _redisSessionService.DeleteTokenAsync(request.UserId);
        return Unit.Value;
    }
}