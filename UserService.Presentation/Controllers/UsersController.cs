using Common.Configurations.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Features.Queries.GetUser;

namespace UserService.Presentation.Controllers;

public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [ValidateSession]
    [HttpGet("users/{userId}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid userId)
    {
        return Ok(await _mediator.Send(new GetUserCommand(userId)));
    }
    
    
}