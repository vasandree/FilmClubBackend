using MediatR;
using UserService.Application.Dtos.Responses;

namespace UserService.Application.Features.Queries.GetUserRoles;

public record GetUserRolesCommand(Guid UserId): IRequest<RolesDto>;