using MediatR;
using UserService.Application.Dtos.Responses;

namespace UserService.Application.Features.Queries.GetUser;

public record GetUserCommand(Guid UserId): IRequest<UserDto>;