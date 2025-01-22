using System.ComponentModel.DataAnnotations;
using UserService.Domain.Enums;

namespace UserService.Application.Dtos.Requests;

public class LoginUserDto
{
    [Required(ErrorMessage = "Login is required")]
    [MinLength(6, ErrorMessage = "Login must be at least 6 characters")]
    public string Login { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    [MaxLength(30, ErrorMessage = "Password must be between 6 and 30 characters")]
    public string Password { get; set; }

    public bool RememberMe { get; set; } = false;
}