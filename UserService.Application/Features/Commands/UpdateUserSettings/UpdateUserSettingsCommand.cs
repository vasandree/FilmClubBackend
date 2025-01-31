using MediatR;
using UserService.Application.Dtos.Responses;

namespace UserService.Application.Features.Commands.UpdateUserSettings;

public record UpdateUserSettingsCommand(Guid UserId, UserSettingsDto UserSettings): IRequest<Unit>;