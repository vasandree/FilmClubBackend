using System.ComponentModel.DataAnnotations;

namespace UserService.Application.Dtos.Responses;

public class RolesDto
{
    [Required]
    public List<string> Roles { get; set; } = [];
}