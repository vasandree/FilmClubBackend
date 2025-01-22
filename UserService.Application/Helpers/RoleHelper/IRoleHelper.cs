using UserService.Domain.Enums;

namespace UserService.Application.Helpers.RoleHelper;

public interface IRoleHelper
{
    List<Guid> GetListOfResourceId(Guid userId, Role role);
    Role ParseRoleString(string roleString);
}