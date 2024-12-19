using AutoMapper;
using Common.Models.Models.Exceptions;
using MediatR;
using UserService.Application.Dtos.Responses;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Queries.GetUser;

public class GetUserHandler : IRequestHandler<GetUserCommand, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IApplicationUserRepository _applicationUserRepository;

    public GetUserHandler(IApplicationUserRepository applicationUserRepository, IMapper mapper)
    {
        _applicationUserRepository = applicationUserRepository;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        if(!await _applicationUserRepository.ExistsAsync(request.UserId))
            throw new BadRequest($"User with id: {request.UserId} does not exist");
        
        var user = await _applicationUserRepository.GetByIdAsync(request.UserId);
        return _mapper.Map<UserDto>(user);
        
    }
}