using System.ComponentModel.DataAnnotations;
using Common.Models;

namespace UserService.Domain.Entities;

public class Role: BaseEntity
{
    [Key] [Required] public Guid Id { get; set; }

    [Required] public Enums.Role RoleName { get; set; }
    
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}