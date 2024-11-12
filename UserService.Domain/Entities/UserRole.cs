using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;

public class UserRole(ApplicationUser user, Role role)
{
    [Required] [ForeignKey("User")] public Guid UserId { get; set; } = user.Id;

    [Required] [ForeignKey("Role")] public Guid RoleId { get; set; } = role.Id;

    [Required] public Role Role { get; set; } = role;

    [Required] public ApplicationUser User { get; set; } = user;
}