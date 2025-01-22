using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;

public abstract class BaseUserRoleEntity
{
    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    
    public ApplicationUser User { get; set; }

}