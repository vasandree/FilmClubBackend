using AutoMapper;
using Common.Models.Models.Exceptions;
using MediatR;
using UserService.Application.Helpers;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Commands.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IChecker _checker;
    private readonly IApplicationUserRepository _applicationUserRepository;
    

    public CreateUserHandler(IApplicationUserRepository applicationUserRepository, IMapper mapper, IChecker checker)
    {
        _applicationUserRepository = applicationUserRepository;
        _mapper = mapper;
        _checker = checker;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userToAdd = request.NewUser;
        
       await _checker.CheckEmailExistsAsync(userToAdd.Email);

       await _checker.CheckUserNameExistsAsync(userToAdd.Username);
        
        var newUser = _mapper.Map<ApplicationUser>(userToAdd);
        await _applicationUserRepository.AddUser(newUser, userToAdd.Password);
        return newUser.Id;
    }
}