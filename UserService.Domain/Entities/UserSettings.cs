using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserService.Domain.Enums;

namespace UserService.Domain.Entities;

public class UserSettings
{
    [Key]
    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    
    public bool DarkMode {get; set;} = false;
    
    public Language PreferredLanguage { get; set; } = Language.En;
    
    public bool OnlyForKids { get; set; } = false;
    
    public ApplicationUser User { get; set; }
}