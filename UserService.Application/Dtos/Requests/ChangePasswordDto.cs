using System.ComponentModel.DataAnnotations;

namespace UserService.Application.Dtos.Requests;

public class ChangePasswordDto
{
    [Required]
    [DataType(DataType.Password)]
    [Length(6,30)]
    public string OldPassword { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [Length(6,30)]
    public string NewPassword { get; set; }
}