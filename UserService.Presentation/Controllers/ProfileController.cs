using Common.Configurations.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Dtos.Requests;
using UserService.Application.Dtos.Responses;
using UserService.Application.Features.Commands.ChangeAvatar;
using UserService.Application.Features.Commands.ChangePassword;
using UserService.Application.Features.Commands.EditUser;
using UserService.Application.Features.Commands.UpdateUserSettings;
using UserService.Application.Features.Queries.GetSettings;
using UserService.Application.Features.Queries.GetUser;

namespace UserService.Presentation.Controllers;

[Authorize]
[ValidateSession]
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

    [HttpPut("avatar")]
    public async Task<IActionResult> UploadAvatar(IFormFile file)
    {
        return Ok(await _mediator.Send(new ChangeAvatarCommand(Guid.Parse(User.FindFirst("UserId")!.Value), file)));
    }

    [HttpGet("settings")]
    public async Task<IActionResult> GetSettings()
    {
        return Ok(await _mediator.Send(new GetUserSettingsCommand(Guid.Parse(User.FindFirst("UserId")!.Value))));
    }

    [HttpPut("settings")]
    public async Task<IActionResult> EditSettings([FromBody] UserSettingsDto userSettings)
    {
        return Ok(await _mediator.Send(new UpdateUserSettingsCommand(Guid.Parse(User.FindFirst("UserId")!.Value),
            userSettings)));
    }
}