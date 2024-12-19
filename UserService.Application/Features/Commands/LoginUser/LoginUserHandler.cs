using Common.Models.Models.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Commands.LoginUser;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, Guid>
{
    private readonly IApplicationUserRepository _applicationUserRepository;

    public LoginUserHandler(IApplicationUserRepository applicationUserRepository,
        SignInManager<ApplicationUser> signInManager)
    {
        _applicationUserRepository = applicationUserRepository;
    }

    public async Task<Guid> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _applicationUserRepository.EmailExistsAsync(request.LoginUserDto.Login) &&
            !await _applicationUserRepository.UserNameExistsAsync(request.LoginUserDto.Login))
            throw new BadRequest("Invalid login or password");

        var user = await _applicationUserRepository.GetByEmailAsync(request.LoginUserDto.Login) ??
                   await _applicationUserRepository.GetByUserNameAsync(request.LoginUserDto.Login);
        if (!await _applicationUserRepository.CheckPasswordAsync(user!, request.LoginUserDto.Password))
            throw new BadRequest("Invalid password");
        
        user!.RememberMe = request.LoginUserDto.RememberMe;
        await _applicationUserRepository.UpdateAsync(user);
        
        return user.Id;
    }
}