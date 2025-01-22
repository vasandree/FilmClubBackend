using Common.Configurations.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Features.Queries.GetUserRoles;

namespace UserService.Presentation.Controllers;

[Authorize]
[ValidateSession]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles()
    {
        return Ok(await _mediator.Send(new GetUserRolesCommand(Guid.Parse(User.FindFirst("UserId")!.Value))));
    }
}