using System.ComponentModel.DataAnnotations;
using Common.Models.Models.ValidationAttributes;
using UserService.Domain.Enums;

namespace UserService.Application.Dtos.Responses;

public class UserDto
{
    [Required(ErrorMessage = "User name is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }
    
    public Gender? Gender { get; set; }

    [DateNotInFuture]
    public DateTime? BirthDate { get; set; }
}