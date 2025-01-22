using MediatR;
using UserService.Application.Dtos.Responses;
using UserService.Domain.Interfaces;
using UserService.Application.Helpers;
using UserService.Application.Helpers.RoleHelper;
using UserService.Domain.Enums;

namespace UserService.Application.Features.Queries.GetUserRoles;

public class GetUserRolesHandler : IRequestHandler<GetUserRolesCommand, RolesDto>
{
    private readonly IChecker _checker;
    private readonly IRoleHelper _roleHelper;
    private readonly IApplicationUserRepository _applicationUserRepository;

    public GetUserRolesHandler(IApplicationUserRepository applicationUserRepository, IChecker checker, IRoleHelper roleHelper)
    {
        _applicationUserRepository = applicationUserRepository;
        _checker = checker;
        _roleHelper = roleHelper;
    }

    public async Task<RolesDto> Handle(GetUserRolesCommand request, CancellationToken cancellationToken)
    {
        await _checker.CheckUserExistsAsync(request.UserId);
        
        var user  = await _applicationUserRepository.GetByIdAsync(request.UserId);
        var userRoles = await _applicationUserRepository.GetRolesAsync(user!);

        var roleAssignments = userRoles
            .Select(role => ParseRole(role, request.UserId))
            .ToList();
        
        return new RolesDto
        {
            Roles = roleAssignments
        };
    }

    protected virtual RolesAssignmentDto ParseRole(string role, Guid userId)
    {
        if (role != Role.Admin.ToString() || role != Role.Manager.ToString())
        {
            return new RolesAssignmentDto()
            {
                Role = role,
                ResourceIds = _roleHelper.GetListOfResourceId(userId, _roleHelper.ParseRoleString(role)),
            };
        }

        return new RolesAssignmentDto()
        {
            Role = role,
        };
    }
}
