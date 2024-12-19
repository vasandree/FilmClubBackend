using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Dtos.Requests;
using UserService.Application.Features.Commands.CreateTokens;
using UserService.Application.Features.Commands.CreateUser;
using UserService.Application.Features.Commands.LoginUser;
using UserService.Application.Features.Commands.LogoutUser;

namespace UserService.Presentation.Controllers;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto registerUserDto)
    {
        var userId = await _mediator.Send(new CreateUserCommand(registerUserDto));
        return Ok(await _mediator.Send(
            new CreateTokensCommand(userId)));
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
    {
        return Ok(await _mediator.Send(
            new CreateTokensCommand(await _mediator.Send(new LoginUserCommand(loginUserDto)))));
    }

    [Authorize]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] string? refreshToken)
    {
        return Ok(await _mediator.Send(new CreateTokensCommand(Guid.Parse(User.FindFirst("UserId")!.Value),
            refreshToken)));
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> LogoutUser([FromBody] string? token)
    {
        return Ok(await _mediator.Send(new LogoutUserCommand(Guid.Parse(User.FindFirst("UserId")!.Value!), token)));
    }
}