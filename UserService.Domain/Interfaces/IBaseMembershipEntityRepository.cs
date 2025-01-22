using UserService.Domain.Enums;

namespace UserService.Domain.Interfaces;

public interface IBaseMembershipEntityRepository
{
    List<Guid> GetListOfResourceId(Guid userId, Role role);
}