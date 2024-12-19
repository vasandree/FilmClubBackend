using System.ComponentModel.DataAnnotations;
using Common.Models.Models.ValidationAttributes;
using UserService.Domain.Enums;

namespace UserService.Application.Dtos.Requests;

public class RegisterUserDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Username is required")]
    [MinLength(6, ErrorMessage = "Username must be at least 6 characters")]
    [MaxLength(30, ErrorMessage = "Username must be between 6 and 30 characters")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    [MaxLength(30, ErrorMessage = "Password must be between 6 and 30 characters")]
    public string Password { get; set; }

    public Gender? Gender { get; set; }

    [DateNotInFuture]
    public DateTime? BirthDate { get; set; }

    public bool RememberMe { get; set; } = false;
    
}