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

    public RefreshToken(ApplicationUser user, string? token, DateTime expirationDate)
    {
        User = user ?? throw new ArgumentNullException(nameof(user));
        UserId = user.Id;
        Token = token;
        ExpireTime = expirationDate;
    }

    public RefreshToken()
    {
    }
}
