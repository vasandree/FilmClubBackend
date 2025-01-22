using AutoMapper;
using Common.Models.Models.Exceptions;
using MediatR;
using UserService.Application.Helpers;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Commands.ChangePassword;

public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, Unit>
{
    private readonly IChecker _checker;
    private readonly IApplicationUserRepository _applicationUserRepository;

    public ChangePasswordHandler(IChecker checker, IApplicationUserRepository applicationUserRepository)
    {
        _checker = checker;
        _applicationUserRepository = applicationUserRepository;
    }

    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        await _checker.CheckUserExistsAsync(request.UserId);

        var user = await _applicationUserRepository.GetByIdAsync(request.UserId);

        await _checker.CheckPasswordAsync(user, request.ChangePasswordDto.OldPassword);

        await _applicationUserRepository.ChangePasswordAsync(user, request.ChangePasswordDto.OldPassword,
            request.ChangePasswordDto.NewPassword);

        return Unit.Value;
    }
}