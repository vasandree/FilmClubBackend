using Common.Models.Models.Exceptions;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Application.Helpers;

public class Checker : IChecker
{
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public Checker(IApplicationUserRepository applicationUserRepository, IRefreshTokenRepository refreshTokenRepository)
    {
        _applicationUserRepository = applicationUserRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task CheckUserExistsAsync(Guid userId)
    {
        if (!await _applicationUserRepository.ExistsAsync(userId))
            throw new BadRequest($"User with id={userId} does not exist");
    }

    public async Task CheckPasswordAsync(ApplicationUser applicationUser, string password)
    {
        if (!await _applicationUserRepository.CheckPasswordAsync(applicationUser, password))
            throw new BadRequest("Old password is incorrect.");
    }


    public async Task CheckRefreshTokenExistsAsync(string refreshToken)
    {
        if (!await _refreshTokenRepository.ExistsAsync(refreshToken))
            throw new BadRequest("Provided refresh token does not exist.");
    }

    public async Task CheckEmailExistsAsync(string email)
    {
        if (await _applicationUserRepository.EmailExistsAsync(email))
            throw new Conflict("User with this email already exists");
    }

    public async Task CheckUserNameExistsAsync(string userName)
    {
        if (await _applicationUserRepository.UserNameExistsAsync(userName))
            throw new Conflict("User with this username already exists");
    }
}