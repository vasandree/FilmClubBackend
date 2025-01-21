
using UserService.Domain.Entities;

namespace UserService.Application.Helpers;

public interface IChecker
{
    Task CheckUserExistsAsync(Guid userId);
    Task CheckPasswordAsync(ApplicationUser applicationUser, string password);
    Task CheckRefreshTokenExistsAsync(string refreshToken);
    Task CheckEmailExistsAsync(string email);
    Task CheckUserNameExistsAsync(string userName);
    
}