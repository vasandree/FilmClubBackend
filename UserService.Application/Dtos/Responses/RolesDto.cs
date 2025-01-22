using System.ComponentModel.DataAnnotations;
using UserService.Domain.Enums;

namespace UserService.Application.Dtos.Responses;

public class RolesDto
{
    [Required]
    public List<RolesAssignmentDto> Roles { get; set; } = [];
}

