using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;

public class RefreshToken
{
    [Key]
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }

    [Required]
    public string? Token { get; set; }

    [Required]
    public DateTime ExpireTime { get; set; }

    [Required]
    public ApplicationUser User { get; set; }
    
}
