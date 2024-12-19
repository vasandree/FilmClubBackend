using Common.Models.Models.Exceptions;
using MediatR;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Commands.ChangePassword;

public class ChangePasswordHandler: IRequestHandler<ChangePasswordCommand, Unit>
{
    private readonly IApplicationUserRepository _applicationUserRepository;

    public ChangePasswordHandler(IApplicationUserRepository applicationUserRepository)
    {
        _applicationUserRepository = applicationUserRepository;
    }

    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        if (!await _applicationUserRepository.ExistsAsync(request.UserId))
            throw new BadRequest($"User with id={request.UserId} does not exist");
        
        var user = await _applicationUserRepository.GetByIdAsync(request.UserId);

        if(!await _applicationUserRepository.CheckPasswordAsync(user, request.ChangePasswordDto.OldPassword))
            throw new BadRequest("Old password is incorrect.");

        await _applicationUserRepository.ChangePasswordAsync(user, request.ChangePasswordDto.OldPassword,
            request.ChangePasswordDto.NewPassword);
        
        return Unit.Value;
    }
}