
using System.ComponentModel.DataAnnotations;
using Common.Models;
using UserService.Domain.Enums;

namespace UserService.Domain.Entities;

public class ApplicationUser: BaseEntity
{
    [Required] public string Username { get; set; }

    [Required] public string Email { get; set; }

    [Required] public string HashedPassword { get; set; }

    [Required] public string FullName { get; set; }

    [Required] public DateTime BirthDate { get; set; }

    [Required] public Gender Gender { get; set; }

    public bool IsEmailConfirmed { get; set; } = false;

    public bool IsBanned { get; set; } = false;
    
    public bool RememberMe { get; set; } = false;

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}