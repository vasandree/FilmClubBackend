using System.ComponentModel.DataAnnotations;
using Common.Models.Models.ValidationAttributes;
using UserService.Domain.Enums;

namespace UserService.Application.Dtos.Responses;

public class UserDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Username { get; set; }
    
    public Gender? Gender { get; set; }
    
    public string AvatarUrl { get; set; }

    [DateNotInFuture]
    public DateTime? BirthDate { get; set; }
}