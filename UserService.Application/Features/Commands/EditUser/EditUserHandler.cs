using AutoMapper;
using Common.Models.Models.Exceptions;
using MediatR;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Commands.EditUser;

public class EditUserHandler : IRequestHandler<EditUserCommand, Unit>
{
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IMapper _mapper;

    public EditUserHandler(IApplicationUserRepository applicationUserRepository, IMapper mapper)
    {
        _applicationUserRepository = applicationUserRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(EditUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _applicationUserRepository.ExistsAsync(request.UserId))
            throw new BadRequest($"User with id={request.UserId} does not exist");
        
        var user = await _applicationUserRepository.GetByIdAsync(request.UserId);
        
        user = _mapper.Map(request.EditProfileDto, user);
        
        await _applicationUserRepository.UpdateAsync(user);
        return Unit.Value;
    }
}