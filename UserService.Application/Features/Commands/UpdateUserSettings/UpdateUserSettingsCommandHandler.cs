using AutoMapper;
using MediatR;
using UserService.Application.Helpers;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Commands.UpdateUserSettings;

public class UpdateUserSettingsCommandHandler : IRequestHandler<UpdateUserSettingsCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IChecker _checker;
    private readonly IUserSettingsRepository _userSettingsRepository;

    public UpdateUserSettingsCommandHandler(IMapper mapper, IChecker checker,
        IUserSettingsRepository userSettingsRepository)
    {
        _mapper = mapper;
        _checker = checker;
        _userSettingsRepository = userSettingsRepository;
    }

    public async Task<Unit> Handle(UpdateUserSettingsCommand request, CancellationToken cancellationToken)
    {
        await _checker.CheckUserExistsAsync(request.UserId);
        var settings = await _userSettingsRepository.GetByUserIdAsync(request.UserId);
        settings = _mapper.Map(request.UserSettings, settings);
        await _userSettingsRepository.UpdateAsync(settings);
        return Unit.Value;
    }
}