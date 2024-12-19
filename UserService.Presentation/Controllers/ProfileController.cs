using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Dtos.Requests;
using UserService.Application.Features.Commands.ChangePassword;
using UserService.Application.Features.Commands.EditUser;
using UserService.Application.Features.Queries.GetUser;

namespace UserService.Presentation.Controllers;

[Authorize]
[Route("profile")]
public class ProfileController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProfileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        return Ok(await _mediator.Send(new GetUserCommand(Guid.Parse(User.FindFirst("UserId")!.Value))));
    }

    [HttpPut]
    public async Task<IActionResult> EditProfile([FromBody] EditProfileDto editProfileDto)
    {
        return Ok(
            await _mediator.Send(new EditUserCommand(Guid.Parse(User.FindFirst("UserId")!.Value), editProfileDto)));
    }

    [HttpPut("change_password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        return Ok(await _mediator.Send(new ChangePasswordCommand(Guid.Parse(User.FindFirst("UserId")!.Value),
            changePasswordDto)));
    }
}