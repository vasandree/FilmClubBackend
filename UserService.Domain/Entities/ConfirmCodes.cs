using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserService.Domain.Entities;

public class ConfirmCodes
{
    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    
    [Required]
    public string Code { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    public DateTime ExpirationDate { get; set; }
    
    [Required]
    public ApplicationUser User { get; set; }
}