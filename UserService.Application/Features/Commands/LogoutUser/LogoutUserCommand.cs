using MediatR;

namespace UserService.Application.Features.Commands.LogoutUser;

public record LogoutUserCommand(Guid UserId, string? RefreshToken) : IRequest<Unit>;