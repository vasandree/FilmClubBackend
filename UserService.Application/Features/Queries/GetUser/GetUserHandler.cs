using AutoMapper;
using Common.Models.Models.Exceptions;
using MediatR;
using UserService.Application.Dtos.Responses;
using UserService.Application.Helpers;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Queries.GetUser;

public class GetUserHandler : IRequestHandler<GetUserCommand, UserDto>
{
    private readonly IMapper _mapper;
    private readonly IChecker _checker;
    private readonly IApplicationUserRepository _applicationUserRepository;

    public GetUserHandler(IApplicationUserRepository applicationUserRepository, IMapper mapper, IChecker checker)
    {
        _applicationUserRepository = applicationUserRepository;
        _mapper = mapper;
        _checker = checker;
    }

    public async Task<UserDto> Handle(GetUserCommand request, CancellationToken cancellationToken)
    {
        await _checker.CheckUserExistsAsync(request.UserId);
        var user = await _applicationUserRepository.GetByIdAsync(request.UserId);
        return _mapper.Map<UserDto>(user);
    }
}