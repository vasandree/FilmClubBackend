namespace UserService.Domain.Entities;

public abstract class BaseMembershipEntity: BaseUserRoleEntity
{
    public Guid ResourceId { get; set; }
}
