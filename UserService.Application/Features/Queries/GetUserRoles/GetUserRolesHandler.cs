using Common.Models.Models.Exceptions;
using MediatR;
using UserService.Application.Dtos.Responses;
using UserService.Domain.Interfaces;

namespace UserService.Application.Features.Queries.GetUserRoles;

public class GetUserRolesHandler : IRequestHandler<GetUserRolesCommand, RolesDto>
{
    private readonly IApplicationUserRepository _applicationUserRepository;

    public GetUserRolesHandler(IApplicationUserRepository applicationUserRepository)
    {
        _applicationUserRepository = applicationUserRepository;
    }

    public async Task<RolesDto> Handle(GetUserRolesCommand request, CancellationToken cancellationToken)
    {
        if (!await _applicationUserRepository.ExistsAsync(request.UserId))
            throw new BadRequest($"User with id {request.UserId} does not exist");

        return new RolesDto()
        {
            Roles = (List<string>)await _applicationUserRepository.GetRolesAsync(
                (await _applicationUserRepository.GetByIdAsync(request.UserId))!)
        };
    }
}