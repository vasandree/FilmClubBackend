using MediatR;
using UserService.Application.Dtos.Responses;

namespace UserService.Application.Features.Queries.GetSettings;

public record GetUserSettingsCommand(Guid UserId): IRequest<UserSettingsDto>;