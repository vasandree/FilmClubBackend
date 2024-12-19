using AutoMapper;
using Common.Models.Models.Exceptions;
using MediatR;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IMapper _mapper;

    public CreateUserHandler(IApplicationUserRepository applicationUserRepository, IMapper mapper)
    {
        _applicationUserRepository = applicationUserRepository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userToAdd = request.NewUser;
        
        if(await _applicationUserRepository.EmailExistsAsync(userToAdd.Email))
            throw new Conflict("User with this email already exists");
        
        if(await _applicationUserRepository.UserNameExistsAsync(userToAdd.Username))
            throw new Conflict("User with this username already exists");
        
        var newUser = _mapper.Map<ApplicationUser>(userToAdd);
        await _applicationUserRepository.AddUser(newUser, userToAdd.Password);
        return newUser.Id;
    }
}