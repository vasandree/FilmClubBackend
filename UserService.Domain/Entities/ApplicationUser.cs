using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using UserService.Domain.Enums;

namespace UserService.Domain.Entities;

public class ApplicationUser: IdentityUser<Guid>
{
    [Required] 
    public string Username { get; set; }

    [Required] 
    public string FullName { get; set; }

    public DateTime? BirthDate { get; set; }
    
    public Gender? Gender { get; set; }

    public bool IsBanned { get; set; } = false;
    
    public bool IsDeleted { get; set; } = false;
    
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}