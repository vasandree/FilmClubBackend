using System.ComponentModel.DataAnnotations;
using UserService.Domain.Enums;

namespace UserService.Application.Dtos.Responses;

public class UserSettingsDto
{
    [Required]
    public bool DarkMode {get; set;}
    
    [Required]
    public Language PreferredLanguage { get; set; }
    
    [Required]
    public bool OnlyForKids { get; set; }
}