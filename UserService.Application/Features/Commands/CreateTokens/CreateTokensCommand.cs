using MediatR;
using UserService.Application.Dtos.Responses;

namespace UserService.Application.Features.Commands.CreateTokens;

public record CreateTokensCommand(Guid UserId, string? RefreshToken = null): IRequest<TokensDto>;