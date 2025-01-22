using UserService.Domain.Enums;
using UserService.Domain.Interfaces;

namespace UserService.Application.Helpers.RoleHelper;

public class RoleHelper : IRoleHelper
{
    private readonly IBaseMembershipEntityRepository _repository;

    public RoleHelper(IBaseMembershipEntityRepository repository)
    {
        _repository = repository;
    }

    public List<Guid> GetListOfResourceId(Guid userId, Role role)
    {
        return _repository.GetListOfResourceId(userId, role);
    }

    public Role ParseRoleString(string roleString)
    {
        if (Enum.TryParse<Role>(roleString, true, out var parsedRole))
        {
            return parsedRole;
        }

        throw new ArgumentException($"Invalid role: {roleString}");
    }
}