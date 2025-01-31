using AutoMapper;
using MediatR;
using UserService.Application.Dtos.Responses;
using UserService.Application.Helpers;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Queries.GetSettings;

public class GetUserSettingsCommandHandler : IRequestHandler<GetUserSettingsCommand, UserSettingsDto>
{
    private readonly IMapper _mapper;
    private readonly IChecker _checker;
    private readonly IUserSettingsRepository _userSettingsRepository;

    public GetUserSettingsCommandHandler(IMapper mapper, IChecker checker, IUserSettingsRepository userSettingsRepository)
    {
        _mapper = mapper;
        _checker = checker;
        _userSettingsRepository = userSettingsRepository;
    }

    public async Task<UserSettingsDto> Handle(GetUserSettingsCommand request, CancellationToken cancellationToken)
    {
        await _checker.CheckUserExistsAsync(request.UserId);
        var settings = await _userSettingsRepository.GetByUserIdAsync(request.UserId);
        return _mapper.Map<UserSettingsDto>(settings);
    }
}