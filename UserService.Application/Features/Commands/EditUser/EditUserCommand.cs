using MediatR;
using UserService.Application.Dtos.Requests;

namespace UserService.Application.Features.Commands.EditUser;

public record EditUserCommand(Guid UserId, EditProfileDto EditProfileDto): IRequest<Unit>;