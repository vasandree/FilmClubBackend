using MediatR;
using UserService.Application.Dtos.Requests;

namespace UserService.Application.Features.Commands.ChangePassword;

public record ChangePasswordCommand(Guid UserId, ChangePasswordDto ChangePasswordDto): IRequest<Unit>;