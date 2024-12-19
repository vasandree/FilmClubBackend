using MediatR;
using UserService.Application.Dtos.Requests;
using UserService.Domain.Entities;

namespace UserService.Application.Features.Commands.LoginUser;

public record LoginUserCommand(LoginUserDto LoginUserDto): IRequest<Guid>;