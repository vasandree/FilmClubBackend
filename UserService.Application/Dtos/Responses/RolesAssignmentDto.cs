using System.ComponentModel.DataAnnotations;
using UserService.Domain.Enums;

namespace UserService.Application.Dtos.Responses;

public class RolesAssignmentDto
{
    [Required]
    public string Role { get; set; }
    
    public List<Guid> ResourceIds {get; set;} = [];
}