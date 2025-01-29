using MediatR;
using UserService.Application.Helpers;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.CloudStorage;

namespace UserService.Application.Features.Commands.ChangeAvatar;

public class ChangeAvatarCommandHandler : IRequestHandler<ChangeAvatarCommand, Unit>
{
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly ICloudStorageService _cloudStorageService;
    private readonly IChecker _checker;

    public ChangeAvatarCommandHandler(IApplicationUserRepository applicationUserRepository, IChecker checker, ICloudStorageService cloudStorageService)
    {
        _applicationUserRepository = applicationUserRepository;
        _checker = checker;
        _cloudStorageService = cloudStorageService;
    }

    public async Task<Unit> Handle(ChangeAvatarCommand request, CancellationToken cancellationToken)
    {
       await _checker.CheckUserExistsAsync(request.UserId);
       
       var user = await _applicationUserRepository.GetByIdAsync(request.UserId);

       
       user!.AvatarUrl = await _cloudStorageService.UploadFileAsync(request.Image, user.Id);
       await _applicationUserRepository.UpdateAsync(user);
       
       return Unit.Value;
    }
}