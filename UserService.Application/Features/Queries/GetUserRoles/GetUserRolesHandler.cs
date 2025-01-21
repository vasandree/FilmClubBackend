using Common.Models.Models.Exceptions;
using MediatR;
using UserService.Application.Dtos.Responses;
using UserService.Application.Helpers;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Queries.GetUserRoles;

public class GetUserRolesHandler : IRequestHandler<GetUserRolesCommand, RolesDto>
{
    private readonly IChecker _checker;
    private readonly IApplicationUserRepository _applicationUserRepository;

    public GetUserRolesHandler(IApplicationUserRepository applicationUserRepository, IChecker checker)
    {
        _applicationUserRepository = applicationUserRepository;
        _checker = checker;
    }

    public async Task<RolesDto> Handle(GetUserRolesCommand request, CancellationToken cancellationToken)
    {
        await _checker.CheckUserExistsAsync(request.UserId);

        return new RolesDto()
        {
            Roles = (List<string>)await _applicationUserRepository.GetRolesAsync(
                (await _applicationUserRepository.GetByIdAsync(request.UserId))!)
        };
    }
}