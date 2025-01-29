using MediatR;
using Microsoft.AspNetCore.Http;

namespace UserService.Application.Features.Commands.ChangeAvatar;

public record ChangeAvatarCommand(Guid UserId, IFormFile Image) : IRequest<Unit>;