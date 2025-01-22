using System.ComponentModel.DataAnnotations;

namespace UserService.Application.Dtos.Requests;

public class ConfirmCodeDto
{
    [Required]
    [Length(6,6)]
    public string Code { get; set; }
}