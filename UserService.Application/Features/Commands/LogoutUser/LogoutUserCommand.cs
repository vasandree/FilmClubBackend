using MediatR;

namespace UserService.Application.Features.Commands.LogoutUser;

public record LogoutUserCommand(string SessionId, Guid UserId, string? RefreshToken) : IRequest<Unit>;