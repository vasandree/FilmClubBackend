using MediatR;
using UserService.Application.Dtos.Requests;
using UserService.Domain.Entities;

namespace UserService.Application.Features.Commands.CreateUser;

public record CreateUserCommand(RegisterUserDto NewUser) : IRequest<Guid>;