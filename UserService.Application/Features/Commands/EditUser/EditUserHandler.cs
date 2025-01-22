using AutoMapper;
using Common.Models.Models.Exceptions;
using MediatR;
using UserService.Application.Helpers;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Commands.EditUser;

public class EditUserHandler : IRequestHandler<EditUserCommand, Unit>
{
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IMapper _mapper;
    private readonly IChecker _checker;

    public EditUserHandler(IApplicationUserRepository applicationUserRepository, IMapper mapper, IChecker checker)
    {
        _applicationUserRepository = applicationUserRepository;
        _mapper = mapper;
        _checker = checker;
    }

    public async Task<Unit> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        await _checker.CheckUserExistsAsync(request.UserId);
        
        var user = await _applicationUserRepository.GetByIdAsync(request.UserId);
        
        user = _mapper.Map(request.EditProfileDto, user);
        
        await _applicationUserRepository.UpdateAsync(user);
        return Unit.Value;
    }
}