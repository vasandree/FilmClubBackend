using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;

public class RefreshToken(ApplicationUser user, string token, DateTime expirationDate)
{
    [Key] [Required] public Guid Id { get; set; } = Guid.NewGuid();

    [Required] [ForeignKey("User")] public Guid UserId { get; set; } = user.Id;

    [Required] public string Token { get; set; } = token;

    [Required] public DateTime ExpireTime { get; set; } = expirationDate;

    [Required] public ApplicationUser User { get; set; } = user;
}